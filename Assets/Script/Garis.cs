using UnityEngine;

public class Garis : MonoBehaviour
{
    private bool isDragging = false;
    private bool isDropped = false; // Menandakan apakah objek sudah di-drop atau belum
    private Vector2 startPosition;

    // Menambahkan referensi ke KerangkaManager
    public KerangkaManager kerangkaManager;

    void Update()
    {
        // Hanya izinkan objek digerakkan jika belum di-drop
        if (isDragging && !isDropped)
        {
            // Mengikuti posisi mouse atau sentuhan layar
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }

    void OnMouseDown()
    {
        // Hanya izinkan objek digerakkan jika belum di-drop
        if (!isDropped)
        {
            isDragging = true;
            startPosition = transform.position;
        }
    }

    void OnMouseUp()
    {
        // Hanya lakukan operasi jika objek belum di-drop
        if (!isDropped)
        {
            isDragging = false;

            // Mendeteksi collision dengan semua objek yang memiliki Collider2D
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
            bool isDroppedOnDropArea = false;

            foreach (Collider2D collider in colliders)
            {
                // Periksa apakah objek garis di-drop di atas objek drop area
                if (collider.CompareTag("DropAreaGaris"))
                {
                    // Objek di-drop pada area yang tepat, sesuaikan posisi
                    transform.position = collider.transform.position;

                    // Hancurkan objek drop area
                    Destroy(collider.gameObject);
                    isDroppedOnDropArea = true;

                    // Menambah jumlah drop area yang dihancurkan
                    kerangkaManager.IncrementDropAreaCountGaris();
                    break;
                }
            }

            // Jika objek tidak di-drop pada drop area, biarkan objek tetap pada posisi terakhirnya
            if (!isDroppedOnDropArea)
            {
                // Tidak perlu kembali ke posisi awal
            }

            // Set isDropped menjadi true hanya jika objek di-drop pada drop area
            if (isDroppedOnDropArea)
            {
                isDropped = true;
            }

            // Perbarui teks info
            kerangkaManager.UpdateInfoText();
        }
    }
}
