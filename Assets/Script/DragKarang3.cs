using UnityEngine;

public class DragKarang3 : MonoBehaviour
{
    private bool isDragging = false;
    private bool isDropped = false; // Menandakan apakah objek sudah di-drop atau belum
    private Vector2 startPosition;

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
                // Periksa apakah objek karang di-drop di atas objek drop area karang
                if (collider.CompareTag("DropAreakarang3"))
                {
                    kerangkaManager.kepasangAudio.Play();
                    // Mengaktifkan semua children dari DropAreaKarang
                    Transform dropAreaTransform = collider.transform;
                    for (int i = 0; i < dropAreaTransform.childCount; i++)
                    {
                        dropAreaTransform.GetChild(i).gameObject.SetActive(true);
                    }

                    // Hancurkan objek karang
                    Destroy(gameObject);
                    isDroppedOnDropArea = true;

                    kerangkaManager.checkImageKarang3.SetActive(true);
                    kerangkaManager.Invoke("DelayedObjectiveActions4", 0.5f);
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
        }
    }
}
