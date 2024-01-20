using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TrashBinManager : MonoBehaviour
{
    public GameObject organicBin;
    public GameObject inorganicBin;
    public GameObject popUpObject;

    public GameObject[] trashPrefabs;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public TextMeshProUGUI remainingTrashText;
    public TextMeshProUGUI scoreText;
    public DialogPlayPilSam dialogPlayPilSam;
    public TMP_Text ScoreFinal;
    public TMP_Text CorrectInput;
    public TMP_Text WrongInput;
    public TMP_Text CongratulationText;

    public AudioSource audioBenar;
    public AudioSource audioSalah;
    public AudioSource audioSpawn;

    private int score = 0;
    private int remainingTrashCount;
    private int correctInputCount = 0;
    private int wrongInputCount = 0;

    private bool isTrashSpawnAllowed = true;
    private bool isTrashDestroyed = true;
    private List<GameObject> spawnedTrash = new List<GameObject>();
    private Coroutine currentMessageCoroutine;
    private bool isFirstTrashButtonPress = true;
    private int highScore;

    private string playerNameKey = "PlayerName";
    private string highScoreL1Key = "HighScoreL1";

    private void Start()
    {
        remainingTrashCount = trashPrefabs.Length;
        UpdateRemainingTrashText();
        UpdateScoreText();
        EnqueueMessage(TypeNewMessage("Tekan tempat sampah warna hitam untuk mengambil sampah. Terus ambil dan pisahkan sesuai jenisnya hingga sampah habis", true));
        
        // Panggil untuk mendapatkan skor tertinggi dari PlayerPrefs saat permainan dimulai
        highScore = PlayerPrefs.GetInt(highScoreL1Key, 0);
    }

    private void UpdateRemainingTrashText()
    {
        if (remainingTrashText != null)
        {
            remainingTrashText.text = remainingTrashCount.ToString();

            if (remainingTrashCount == 0)
            {
                StartCoroutine(ActivatePopUp());
            }
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = Mathf.Max(0, score).ToString();
        }
    }

    private void UpdateFinalScoreText()
    {
        if (ScoreFinal != null)
        {
            ScoreFinal.text = score.ToString();
        }

        if (CorrectInput != null)
        {
            CorrectInput.text = correctInputCount.ToString() + " Buah";
        }

        if (WrongInput != null)
        {
            WrongInput.text = wrongInputCount.ToString() + " Buah";
        }
    }

    private void UpdateCongratulationText()
    {
        string playerName = GetPlayerName();
        if (!string.IsNullOrEmpty(playerName) && CongratulationText != null)
        {
            CongratulationText.text = $"Selamat {playerName}, Kamu berhasil menyelesaikan pelatihan hari ini.";
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
            audioSpawn.Play();

            if (isFirstTrashButtonPress)
            {
                isFirstTrashButtonPress = false;
                EnqueueMessage(TypeNewMessage("Setelah Mengambil Sampah, Masukkan Sampah Ke Tong Baru Sesuai Dengan Jenisnya", true));
            }
        }
    }

    void SpawnTrash()
    {
        if (remainingTrashCount > 0)
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
            correctInputCount++;
            UpdateScoreText();
            audioBenar.Play();
            EnqueueMessage(TypeNewMessage("Bagus, Pilihanmu Benar. Ayo Ambil Sampah Lagi Sampai Habis", true));
        }
        else
        {
            score = Mathf.Max(0, score - 5);
            wrongInputCount++;
            UpdateScoreText();
            audioSalah.Play();
            EnqueueMessage(TypeNewMessage("Ups, Kamu Salah. Ayo Ambil Sampah Lagi Sampai Habis", false));
        }

        remainingTrashCount--;
        UpdateRemainingTrashText();

        isTrashSpawnAllowed = true;

        if (remainingTrashCount == 0)
        {
            UpdateFinalScoreText();
            // Panggil untuk memperbarui high score saat permainan berakhir
            UpdateHighScore();
        }
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

    private IEnumerator ActivatePopUp()
    {
        yield return new WaitForSeconds(1f);

        if (popUpObject != null)
        {
            popUpObject.SetActive(true);
            UpdateFinalScoreText();
            UpdateCongratulationText();
            
            // Panggil untuk memperbarui high score saat pop-up aktif
            UpdateHighScore();
        }
    }

    private void GameOver()
    {
        // Panggil untuk memperbarui high score saat game over
        UpdateHighScore();
        Debug.Log("Game Over");
    }

    public bool IsTrashDestroyed()
    {
        return isTrashDestroyed;
    }

    private string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey, "Player");
    }

    private void UpdateHighScore()
    {
        // Pemeriksaan skor tertinggi sebelum menyimpan ke PlayerPrefs
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreL1Key, highScore);
        }
    }
}
