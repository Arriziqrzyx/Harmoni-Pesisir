using UnityEngine;
using TMPro;
using System.Collections; // Tambahkan ini untuk menggunakan IEnumerator
using DG.Tweening;

public class SeaCleanManager : MonoBehaviour
{
    public TextMeshProUGUI seaweedInfoText; // Referensi ke UI TextMeshPro untuk seaweed
    public TextMeshProUGUI trashInfoText;   // Referensi ke UI TextMeshPro untuk trash

    public float floatSpeed = 1f; // Kecepatan terombang-ambing sampah
    public float floatDistance = 0.5f; // Jarak maksimum terombang-ambing sampah

    public GameObject seaweedCheckObject; // Objek check untuk seaweed
    public GameObject trashCheckObject;   // Objek check untuk trash
    public GameObject successPanel;       // Objek panel keberhasilan

    private int seaweedCleanedCount = 0;
    private int trashCleanedCount = 0;

    private int totalSeaweedCount;
    private int totalTrashCount;

    private void Start()
    {
        // Menghitung jumlah seaweed dan trash secara dinamis
        totalSeaweedCount = CountObjectsWithTag("Seaweed");
        totalTrashCount = CountObjectsWithTag("Trash");

        // Memperbarui teks informasi seaweed dan trash
        UpdateSeaweedInfoText();
        UpdateTrashInfoText();

        // Memulai animasi terombang-ambing untuk sampah
        StartFloatingAnimation();
    }

    // Metode untuk menghitung jumlah objek dengan tag tertentu
    private int CountObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        return objects.Length;
    }

    // Metode untuk memperbarui teks informasi seaweed
    public void SeaweedDestroyed()
    {
        seaweedCleanedCount++;
        UpdateSeaweedInfoText();
        CheckSeaweedCleaned();
    }

    // Metode untuk memperbarui teks informasi trash
    public void TrashDestroyed()
    {
        trashCleanedCount++;
        UpdateTrashInfoText();
        CheckTrashCleaned();
    }

    // Metode untuk memperbarui teks informasi seaweed
    private void UpdateSeaweedInfoText()
    {
        seaweedInfoText.text = "Seaweed dibersihkan " + seaweedCleanedCount + "/" + totalSeaweedCount;
    }

    // Metode untuk memperbarui teks informasi trash
    private void UpdateTrashInfoText()
    {
        trashInfoText.text = "Trash dibersihkan " + trashCleanedCount + "/" + totalTrashCount;
    }

    // Metode untuk memulai animasi terombang-ambing sampah
    private void StartFloatingAnimation()
    {
        // Mencari semua objek trash dalam hierarki
        TrashController[] trashControllers = FindObjectsOfType<TrashController>();

        foreach (TrashController trashController in trashControllers)
        {
            trashController.StartFloatingAnimation(floatSpeed, floatDistance);
        }
    }

    // Metode untuk memeriksa apakah semua seaweed sudah bersih
    private void CheckSeaweedCleaned()
    {
        if (seaweedCleanedCount == totalSeaweedCount)
        {
            ActivateSeaweedCheckObject();
            CheckSuccessCondition();
        }
    }

    // Metode untuk memeriksa apakah semua trash sudah bersih
    private void CheckTrashCleaned()
    {
        if (trashCleanedCount == totalTrashCount)
        {
            ActivateTrashCheckObject();
            CheckSuccessCondition();
        }
    }

    // Metode untuk mengaktifkan objek check seaweed
    private void ActivateSeaweedCheckObject()
    {
        seaweedCheckObject.SetActive(true);
    }

    // Metode untuk mengaktifkan objek check trash
    private void ActivateTrashCheckObject()
    {
        trashCheckObject.SetActive(true);
    }

    // Metode untuk memeriksa apakah semua seaweed dan trash sudah bersih
    private void CheckSuccessCondition()
    {
        if (seaweedCleanedCount == totalSeaweedCount && trashCleanedCount == totalTrashCount)
        {
            StartCoroutine(ActivateSuccessPanelWithDelay(1.5f));
        }
    }

    // Coroutine untuk menunda aktivasi panel keberhasilan
    private IEnumerator ActivateSuccessPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Setelah jeda, aktifkan panel keberhasilan
        ActivateSuccessPanel();
    }

    // Metode untuk mengaktifkan panel keberhasilan
    private void ActivateSuccessPanel()
    {
        successPanel.SetActive(true);
    }
}
