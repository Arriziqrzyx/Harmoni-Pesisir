using UnityEngine;
using System.Collections;
using TMPro;

public enum ObjectState
{
    Moving,
    Stopped
}

public class GerakanNaikTurun : MonoBehaviour
{
    public Transform pointAtas; // Atas
    public Transform pointBawah; // Bawah
    private Vector3 posisiAwal; // Variabel untuk menyimpan posisi awal
    [SerializeField] TMP_Text distanceText;
    public float kecepatan = 2.0f;
    public Collider2D winAreaCollider; // Collider untuk win area
    public GameObject winObject; // Objek yang diaktifkan saat menang
    public GameObject loseObject; // Objek yang diaktifkan saat kalah
    float distance;
    private ObjectState currentState = ObjectState.Moving;
    private float waktuMulai;
    public UkurGameManager ukurGameManager; // Referensi ke GameManager

    void Start()
    {
        // Simpan posisi awal sebelum digerakkan
        posisiAwal = transform.position;
        waktuMulai = Time.time;

        // Mulai gerakan objek dari posisi awal
        StartMovement();
    }

    void Update()
    {
        // Pemrosesan pergerakan hanya jika objek sedang dalam status Moving
        if (currentState == ObjectState.Moving)
        {
            float waktuSekarang = Time.time - waktuMulai;

            // Menggunakan Mathf.PingPong untuk membuat pergerakan yang berulang
            float pergerakan = Mathf.PingPong(waktuSekarang * kecepatan, 1.0f);

            // Gunakan nilai pergerakan untuk menggerakkan objek antara dua posisi (pointBawah dan pointAtas)
            transform.position = Vector3.Lerp(pointBawah.position, pointAtas.position, pergerakan);

            distance = (pointBawah.transform.position - transform.position).magnitude;
            distanceText.text = distance.ToString("F1").Replace(".", "") + "CM";
        }
    }

    public void TombolKlik()
    {
        // Pengecekan jika objek berada di dalam win area
        if (winAreaCollider.bounds.Contains(transform.position))
        {
            // Objek berada di dalam win area, tampilkan konsol sukses
            Debug.Log("Sukses!");
            // Aktifkan objek yang diaktifkan saat menang
            if (winObject != null)
            {
                winObject.SetActive(true);
                ukurGameManager.TambahSkor(25); // Tambahkan skor 25 jika menang
            }
        }
        else
        {
            // Objek berada di luar win area, tampilkan konsol lose
            Debug.Log("Lose!");
            // Aktifkan objek yang diaktifkan saat kalah
            if (loseObject != null)
            {
                loseObject.SetActive(true);
                ukurGameManager.TambahSkor(-5); // Tambahkan skor 25 jika menang
            }
        }

        // Berhenti bergerak setelah diklik
        StopMovement();
    }

    public void Ulangi()
    {
        // Objek kembali ke posisi awal dan mulai bergerak lagi
        RestartObject();
    }

    private void RestartObject()
    {
        // Objek kembali ke posisi awal dan mulai bergerak lagi
        StartMovement();
    }

    void StopMovement()
    {
        currentState = ObjectState.Stopped;
    }

    void StartMovement()
    {
        currentState = ObjectState.Moving;
    }
}
