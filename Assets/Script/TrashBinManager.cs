using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TrashBinManager : MonoBehaviour
{
    public GameObject organicBin;
    public GameObject inorganicBin;

    public GameObject[] trashPrefabs;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public TextMeshProUGUI remainingTrashText;
    public TextMeshProUGUI scoreText;
    public DialogPlayPilSam dialogPlayPilSam;
    private int score = 0;

    private bool isTrashSpawnAllowed = true;
    private bool isTrashDestroyed = true;
    private List<GameObject> spawnedTrash = new List<GameObject>();
    private Coroutine currentMessageCoroutine;
    private bool isFirstTrashButtonPress = true;

    private void Start()
    {
        UpdateRemainingTrashText();
        UpdateScoreText();
        EnqueueMessage(TypeNewMessage("Tekan tempat sampah warna hitam  untuk mengambil sampah. Terus ambil dan pisahkan sesuai jenisnya hingga sampah habis", true));
    }

    private void UpdateRemainingTrashText()
    {
        if (remainingTrashText != null)
        {
            int remainingTrashCount = trashPrefabs.Length - spawnedTrash.Count;
            remainingTrashText.text = remainingTrashCount.ToString();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Skor: " + Mathf.Max(0, score); // Pastikan skor tidak negatif
        }
    }

    void Update()
    {
        // Tombol spawn di UI
    }

    public void TombolSampah()
    {
        if (isTrashSpawnAllowed)
        {
            SpawnTrash();

            if (isFirstTrashButtonPress)
            {
                isFirstTrashButtonPress = false;
                EnqueueMessage(TypeNewMessage("Setelah Mengambil Sampah, Masukkan Sampah Ke Tong Baru Sesuai Dengan Jenisnya", true));
            }
        }
    }

    void SpawnTrash()
    {
        if (spawnedTrash.Count < trashPrefabs.Length)
        {
            isTrashSpawnAllowed = false;

            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, trashPrefabs.Length);
            } while (spawnedTrash.Contains(trashPrefabs[randomIndex]));

            GameObject trash = Instantiate(trashPrefabs[randomIndex], spawnPoint1.position, Quaternion.identity);
            spawnedTrash.Add(trash);

            trash.transform.DOMove(spawnPoint2.position, 1f).OnComplete(() =>
            {
                SetupTrashType(trash);
            });

            UpdateRemainingTrashText();
            isTrashDestroyed = false;
        }
    }

    private void SetupTrashType(GameObject trash)
    {
        TrashType trashType = trash.GetComponent<TrashType>();
        if (trashType != null)
        {
            trashType.SetBins(organicBin, inorganicBin, this);
        }
        else
        {
            Debug.LogError("Component TrashType not found on the trash object.");
        }
    }

    public void TrashEnteredBin(bool isCorrectBin)
    {
        if (isCorrectBin)
        {
            score += 10;
            UpdateScoreText();
            EnqueueMessage(TypeNewMessage("Bagus, Pilihanmu Benar. Ayo Ambil Sampah Lagi Sampai Habis", true));
        }
        else
        {
            score = Mathf.Max(0, score - 5); // Pastikan skor tidak negatif
            UpdateScoreText();
            EnqueueMessage(TypeNewMessage("Ups, Kamu Salah. Ayo Ambil Sampah Lagi Sampai Habis", false));
        }

        isTrashSpawnAllowed = true;
    }

    private IEnumerator TypeNewMessage(string message, bool isCorrect)
    {
        if (dialogPlayPilSam != null)
        {
            yield return dialogPlayPilSam.TypeMessage(message, isCorrect);
        }
    }

    private void EnqueueMessage(IEnumerator coroutine)
    {
        if (currentMessageCoroutine != null)
        {
            StopCoroutine(currentMessageCoroutine);
        }

        currentMessageCoroutine = StartCoroutine(coroutine);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    public bool IsTrashDestroyed()
    {
        return isTrashDestroyed;
    }
}
