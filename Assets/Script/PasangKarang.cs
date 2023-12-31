using UnityEngine;

public class PasangKarang : MonoBehaviour
{
    public int jenisObjek; // Tambahkan variabel untuk menandakan jenis objek
    public SpriteRenderer spriteRendererJenis1; // Tambahkan variabel sprite renderer untuk jenis 1
    public SpriteRenderer spriteRendererJenis2; // Tambahkan variabel sprite renderer untuk jenis 2
    public SpriteRenderer spriteRendererJenis3; // Tambahkan variabel sprite renderer untuk jenis 3
    public SpriteRenderer spriteRendererJenis4; // Tambahkan variabel sprite renderer untuk jenis 4
    public SpriteRenderer spriteRendererJenis5; // Tambahkan variabel sprite renderer untuk jenis 5
    public SpriteRenderer spriteRendererJenis6; // Tambahkan variabel sprite renderer untuk jenis 6

    private bool isDragging = false;
    private Vector2 startPosition;

    void Update()
    {
        if (isDragging)
        {
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
        bool isDroppedOnDropLautKarang = false;

        foreach (Collider2D collider in colliders)
        {
            // Periksa apakah objek karang di-drop di atas objek drop area karang di laut
            if (collider.CompareTag("DropLautKarang"))
            {
                // Objek di-drop pada area karang di laut yang tepat, hancurkan objek karang
                Destroy(gameObject);

                // Tambahkan komponen Sprite Renderer pada objek drop area karang di laut sesuai jenis objek
                SpriteRenderer dropLautKarangSpriteRenderer = collider.gameObject.AddComponent<SpriteRenderer>();

                // Set sprite pada Sprite Renderer dengan jenis objek yang di-drop
                switch (jenisObjek)
                {
                    case 1:
                        dropLautKarangSpriteRenderer.sprite = spriteRendererJenis1.sprite;
                        break;
                    case 2:
                        dropLautKarangSpriteRenderer.sprite = spriteRendererJenis2.sprite;
                        break;
                    case 3:
                        dropLautKarangSpriteRenderer.sprite = spriteRendererJenis3.sprite;
                        break;
                    case 4:
                        dropLautKarangSpriteRenderer.sprite = spriteRendererJenis4.sprite;
                        break;
                    case 5:
                        dropLautKarangSpriteRenderer.sprite = spriteRendererJenis5.sprite;
                        break;
                    case 6:
                        dropLautKarangSpriteRenderer.sprite = spriteRendererJenis6.sprite;
                        break;
                    default:
                        break;
                }

                // Atur order in layer ke 1
                dropLautKarangSpriteRenderer.sortingOrder = 1;

                isDroppedOnDropLautKarang = true;
                break;
            }
        }

        // Jika objek karang tidak di-drop pada drop area karang di laut, biarkan objek tetap pada posisi terakhirnya
        if (!isDroppedOnDropLautKarang)
        {
            // Tidak perlu kembali ke posisi awal
        }
    }
}
