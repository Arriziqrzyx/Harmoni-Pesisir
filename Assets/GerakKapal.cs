using UnityEngine;

public class GerakKapal : MonoBehaviour
{
    public float kecepatanKapal = 5f;
    public float batasHancur = 13f;

    private bool sudahDiklik = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private KapalGameManager gameManager;

    // Tambahkan variabel untuk menentukan jenis kapal
    public bool kapalJahat;

    void Start()
    {
        // Mendapatkan referensi Rigidbody2D, SpriteRenderer, dan KapalGameManager pada awal
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<KapalGameManager>(); // Menggunakan FindObjectOfType karena GameManager adalah satu-satunya objek di scene
        if (gameManager == null)
        {
            Debug.LogError("Tidak ada objek KapalGameManager di scene!");
        }

        // Tentukan arah pergerakan dan flip sprite berdasarkan posisi spawn
        if (transform.position.x < 0)
        {
            // Jika spawn dari kiri, bergerak ke kanan dan flip sprite
            kecepatanKapal = Mathf.Abs(kecepatanKapal);
            spriteRenderer.flipX = true;
        }
        else
        {
            // Jika spawn dari kanan, bergerak ke kiri
            kecepatanKapal = -Mathf.Abs(kecepatanKapal);
        }
    }

    void Update()
    {
        // Bergerak sesuai arah yang telah ditentukan
        transform.Translate(Vector2.right * kecepatanKapal * Time.deltaTime);

        // Hancurkan kapal jika mencapai batas tertentu
        if (transform.position.x >= batasHancur || transform.position.x <= -batasHancur)
        {
            HandleKapalMelewatiBatas();
        }
    }

    void OnMouseDown()
    {
        // Cek apakah kapal sudah diklik
        if (!sudahDiklik)
        {
            // Set posisi Y menjadi 1
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

            // Flip Y dan hentikan pergerakan
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            sudahDiklik = true;

            // Menonaktifkan freeze position Y pada Rigidbody
            rb.constraints = RigidbodyConstraints2D.None;

            // Tentukan nilai skor berdasarkan jenis kapal
            int nilaiSkor = kapalJahat ? 10 : -10;

            // Update skor pada GameManager
            gameManager.UpdateSkor(nilaiSkor);

            // Mulai bergerak turun
            StartFalling();
        }
    }

    void StartFalling()
    {
        // Tunggu selama 1 detik sebelum menghancurkan objek
        Invoke("DestroyAfterDelay", 1f);
    }

    void DestroyAfterDelay()
    {
        // Hancurkan objek
        Destroy(gameObject);
    }

    void HandleKapalMelewatiBatas()
    {
        // Tentukan nilai skor berdasarkan jenis kapal
        int nilaiSkor = kapalJahat ? -10 : 5;

        // Update skor pada GameManager
        gameManager.UpdateSkor(nilaiSkor);

        // Hancurkan objek
        Destroy(gameObject);
    }
}
