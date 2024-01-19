using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TrashBinManager : MonoBehaviour
{
    public GameObject organicBin;
    public GameObject inorganicBin;
    public GameObject popUpObject; // Game object pop-up

    public GameObject[] trashPrefabs;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public TextMeshProUGUI remainingTrashText;
    public TextMeshProUGUI scoreText;
    public DialogPlayPilSam dialogPlayPilSam;
    public TMP_Text ScoreFinal;    // Text for displaying the final score
    public TMP_Text CorrectInput;  // Text for displaying the count of correct inputs
    public TMP_Text WrongInput;    // Text for displaying the count of wrong inputs
    public TMP_Text CongratulationText; // Text for displaying congratulatory message

    public AudioSource audioBenar;
    public AudioSource audioSalah;
    public AudioSource audioSpawn;

    private int score = 0;
    private int remainingTrashCount; // Jumlah sisa sampah
    private int correctInputCount = 0; // Counter for correct inputs
    private int wrongInputCount = 0;   // Counter for wrong inputs

    private bool isTrashSpawnAllowed = true;
    private bool isTrashDestroyed = true;
    private List<GameObject> spawnedTrash = new List<GameObject>();
    private Coroutine currentMessageCoroutine;
    private bool isFirstTrashButtonPress = true;

    // Key for player name in PlayerPrefs
    private string playerNameKey = "PlayerName";

    private void Start()
    {
        // Set jumlah awal sisa sampah
        remainingTrashCount = trashPrefabs.Length;
        
        // Update UI
        UpdateRemainingTrashText();
        UpdateScoreText();
        EnqueueMessage(TypeNewMessage("Tekan tempat sampah warna hitam untuk mengambil sampah. Terus ambil dan pisahkan sesuai jenisnya hingga sampah habis", true));
    }

    private void UpdateRemainingTrashText()
    {
        if (remainingTrashText != null)
        {
            remainingTrashText.text = remainingTrashCount.ToString();

            // Jika sampah habis, tunggu 1 detik kemudian aktifkan pop-up
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
            scoreText.text = Mathf.Max(0, score).ToString(); // Pastikan skor tidak negatif
        }
    }

    private void UpdateFinalScoreText()
    {
        if (ScoreFinal != null)
        {
            ScoreFinal.text = score.ToString() ;
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

            // Tidak mengurangi sisa sampah di sini
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
            score = Mathf.Max(0, score - 5); // Pastikan skor tidak negatif
            wrongInputCount++;
            UpdateScoreText();
            audioSalah.Play();
            EnqueueMessage(TypeNewMessage("Ups, Kamu Salah. Ayo Ambil Sampah Lagi Sampai Habis", false));
        }

        remainingTrashCount--; // Mengurangi sisa sampah setelah sampah dimasukkan ke tong
        UpdateRemainingTrashText();

        isTrashSpawnAllowed = true;

        // Cek apakah semua sampah sudah dimasukkan, update teks final score jika ya
        if (remainingTrashCount == 0)
        {
            UpdateFinalScoreText();
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
            // Aktifkan pop-up
            popUpObject.SetActive(true);

            // Update teks final score dan ucapan selamat pada pop-up
            UpdateFinalScoreText();
            UpdateCongratulationText();
        }
    }

    private void GameOver()
    {
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
}
