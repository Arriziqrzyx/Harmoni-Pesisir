using UnityEngine;

public class Tali : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 startPosition;

    // Menambahkan referensi ke KerangkaManager
    public KerangkaManager kerangkaManager;

    void Update()
    {
        if (isDragging)
        {
            // Mengikuti posisi mouse atau sentuhan layar
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        startPosition = transform.position;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Mendeteksi collision dengan semua objek yang memiliki Collider2D
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
        bool isDroppedOnDropAreaTali = false;

        foreach (Collider2D collider in colliders)
        {
            // Periksa apakah objek tali di-drop di atas objek drop area tali
            if (collider.CompareTag("DropAreaTali"))
            {
                // Objek di-drop pada area tali yang tepat, hancurkan objek tali
                Destroy(gameObject);

                // Aktifkan komponen Sprite Renderer dari objek drop area tali
                SpriteRenderer dropAreaSpriteRenderer = collider.GetComponent<SpriteRenderer>();
                if (dropAreaSpriteRenderer != null)
                {
                    dropAreaSpriteRenderer.enabled = true;
                }

                // Menambah jumlah objek tali yang dihancurkan pada KerangkaManager
                kerangkaManager.IncrementDropAreaCountTali();
                isDroppedOnDropAreaTali = true;
                break;
            }
        }

        // Jika objek tali tidak di-drop pada drop area tali, biarkan objek tetap pada posisi terakhirnya
        if (!isDroppedOnDropAreaTali)
        {
            // Tidak perlu kembali ke posisi awal
        }

        // Perbarui teks info
        kerangkaManager.UpdateInfoText();
    }
}
