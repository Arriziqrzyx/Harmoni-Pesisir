using UnityEngine;

public class PasangKarangManager : MonoBehaviour
{
    public GameObject[] pasangKarangObjects; // Array untuk menyimpan objek PasangKarang

    private int currentIndex = 0; // Indeks objek PasangKarang yang akan diaktifkan selanjutnya

    void Start()
    {
        ActivateNextPasangKarang();
    }

    void Update()
    {
        // Periksa apakah objek PasangKarang sebelumnya sudah di-drop atau di-destroy
        if (currentIndex > 0 && pasangKarangObjects[currentIndex - 1] == null)
        {
            ActivateNextPasangKarang();
        }
    }

    void ActivateNextPasangKarang()
    {
        // Aktifkan objek PasangKarang berikutnya
        if (currentIndex < pasangKarangObjects.Length)
        {
            pasangKarangObjects[currentIndex].SetActive(true);
            currentIndex++;
        }
    }
}
