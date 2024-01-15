using UnityEngine;
using TMPro;

public class KapalGameManager : MonoBehaviour
{
    public GameObject kapalBaikPrefab;
    public GameObject kapalJahatPrefab;
    public Transform spawnPointKiri;
    public Transform spawnPointKanan;
    public TMP_Text skorText;
    public TMP_Text timerText;
    public GameObject panelGameOver; // Panel yang akan diaktifkan saat timer habis

    private int skor = 0;
    private float timer = 150f; // 2 menit 30 detik

    void Start()
    {
        // Panggil fungsi SpawnKapal setiap beberapa detik (misalnya, setiap 2 detik)
        InvokeRepeating("SpawnKapal", 0f, 8f);

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
            // Timer habis, aktifkan panel
            panelGameOver.SetActive(true);
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
            kapalPrefab = kapalBaikPrefab;
        }
        else
        {
            kapalPrefab = kapalJahatPrefab;
        }

        // Instantiate prefab kapal di spawnPoint
        GameObject kapal = Instantiate(kapalPrefab, spawnPoint.position, Quaternion.identity);

        // Tentukan parent agar kapal dapat diorganisir dengan baik
        kapal.transform.parent = transform;
    }

    public void UpdateSkor(int nilai)
    {
        skor += nilai;

        // Pastikan skor tidak negatif
        skor = Mathf.Max(skor, 0);

        skorText.text = "Skor: " + skor.ToString();
    }
}
