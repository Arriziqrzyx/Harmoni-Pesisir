using UnityEngine;
using TMPro;

public class KapalGameManager : MonoBehaviour
{
    public GameObject[] kapalBaikPrefabs;
    public GameObject[] kapalJahatPrefabs;
    public Transform spawnPointKiri;
    public Transform spawnPointKanan;
    public TMP_Text skorText;
    public TMP_Text timerText;
    public GameObject panelGameOver; // Panel yang akan diaktifkan saat timer habis
    public TMP_Text resultMessageText;

    private IkanGameManager ikanGameManager;
    private int skor = 0;
    private float timer = 60f; // 2 menit 30 detik

    private int highScoreL4 = 0;
    private string highScoreL4Key = "HighScoreL4";

    void Start()
    {
        // Ambil high score dari PlayerPrefs
        highScoreL4 = PlayerPrefs.GetInt(highScoreL4Key, 0);

        ikanGameManager = FindObjectOfType<IkanGameManager>(); // Menggunakan FindObjectOfType karena GameManager adalah satu-satunya objek di scene
        if (ikanGameManager == null)
        {
            Debug.LogError("Tidak ada objek IkanGameManager di scene!");
        }

        // Panggil fungsi SpawnKapal setiap beberapa detik (misalnya, setiap 2 detik)
        InvokeRepeating("SpawnKapal", 0f, 3.5f);

        // Mulai mengurangi timer setiap detik
        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    void UpdateTimer()
    {
        if (timer > 0)
        {
            timer -= 1f;

            // Konversi timer ke menit dan detik
            int menit = Mathf.FloorToInt(timer / 60);
            int detik = Mathf.FloorToInt(timer % 60);

            // Tampilkan timer pada TMP_Text
            timerText.text = "Menit: " + menit.ToString("D2") + " : " + detik.ToString("D2");
        }
        else
        {
            // Timer habis, tunggu 1 detik sebelum mengaktifkan panel
            Invoke("AktifkanPanelGameOver", 1f);
        }
    }

    void SpawnKapal()
    {
        // Tentukan posisi spawn secara acak antara kiri dan kanan
        Transform spawnPoint;
        if (Random.Range(0, 2) == 0)
        {
            spawnPoint = spawnPointKiri;
        }
        else
        {
            spawnPoint = spawnPointKanan;
        }

        // Tentukan prefab kapal secara acak antara kapal baik dan kapal jahat
        GameObject kapalPrefab;
        if (Random.Range(0, 2) == 0)
        {
            kapalPrefab = kapalBaikPrefabs[Random.Range(0, kapalBaikPrefabs.Length)];
        }
        else
        {
            kapalPrefab = kapalJahatPrefabs[Random.Range(0, kapalJahatPrefabs.Length)];
        }

        // Instantiate prefab kapal di spawnPoint
        GameObject kapal = Instantiate(kapalPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void UpdateSkor(int nilai)
    {
        skor += nilai;

        // Pastikan skor tidak negatif
        skor = Mathf.Max(skor, 0);

        skorText.text = "Skor: " + skor.ToString();

        // Periksa apakah skor yang baru dicapai lebih tinggi dari high score yang tersimpan
        if (skor > highScoreL4)
        {
            // Jika ya, perbarui high score dan simpan ke PlayerPrefs
            highScoreL4 = skor;
            PlayerPrefs.SetInt(highScoreL4Key, highScoreL4);
        }
    }

    void AktifkanPanelGameOver()
    {
        ikanGameManager.stopIkan();
        // Hentikan spawing
        CancelInvoke("SpawnKapal");

        // Nonaktifkan script GerakKapal pada semua kapal yang ada
        GerakKapal[] kapalArray = FindObjectsOfType<GerakKapal>();
        foreach (GerakKapal kapal in kapalArray)
        {
            kapal.enabled = false;
        }

        if (resultMessageText != null)
        {
            resultMessageText.text = "Bagusss, Kamu sangat tegas dan berani menjaga wilayah konservasi ini dari kapal asing. Skor kamu: " + skor;
        }
        // Aktifkan panel
        panelGameOver.SetActive(true);
        // Menampilkan pesan setelah panel ditampilkan
        
    }
}
