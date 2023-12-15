using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public Question[] questions;
    private int currentQuestionIndex;
    private int correctAnswersCount;
    private int totalQuestions;
    public int defaultTime = 50; // Durasi waktu default

    public TMP_Text questionText;
    public Button[] answerButtons;
    public GameObject successPanel;
    public GameObject failPanel;
    public TMP_Text congratulationText;
    public TMP_Text correctAnswersText;
    public TMP_Text remainingTimeText;
    public TMP_Text totalScoreText;
    public TMP_Text tryAgainText;
    public TMP_Text correctAnswersTextLose;
    public TMP_Text remainingTimeTextLose;
    public TMP_Text totalScoreTextLose;
    public TMP_Text countdownText; // Teks countdown UI
    [SerializeField] private string Scene;

    private Coroutine countdownCoroutine;
    private int currentTime; // Tambahkan variabel currentTime sebagai variabel instans
    private string playerNameKey = "PlayerName";

    private void Start()
    {
        currentQuestionIndex = 0;
        correctAnswersCount = 0;
        totalQuestions = questions.Length;

        LoadQuestion(currentQuestionIndex);
    }

    private void LoadQuestion(int questionIndex)
    {
        questionText.text = questions[questionIndex].question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = questions[questionIndex].answers[i];
        }

        // Cek apakah coroutine sudah berjalan, jika belum, baru jalankan
        if (countdownCoroutine == null)
        {
            currentTime = defaultTime; // Atur currentTime ke nilai defaultTime
            countdownCoroutine = StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            countdownText.text = "Waktu Tersisa: " + currentTime + " detik";
            yield return new WaitForSeconds(1f);
            currentTime--;

            if (currentQuestionIndex >= totalQuestions)
                yield break;
        }

        ShowResult();
    }


    private void ShowResult()
    {
        string playerName = PlayerPrefs.GetString(playerNameKey, "Kamu");

        if (correctAnswersCount >= 3 && currentTime > 0)
        {
            successPanel.SetActive(true);
            congratulationText.text = "Selamat ya " + playerName + ", kamu hebat bisa berhasil menyelesaikan pelatihan ini!";
            correctAnswersText.text = correctAnswersCount + " dari " + totalQuestions;
            remainingTimeText.text = currentTime + " detik";
            totalScoreText.text = (correctAnswersCount * 2 * currentTime).ToString();
        }
        else
        {
            failPanel.SetActive(true);

            if (currentTime == 0)
            {
                tryAgainText.text = "Maaf " + playerName + ", kamu kehabisan waktu! Kamu dapat mengerjakan lebih cepat lagi ya.";
            }
            else
            {
                tryAgainText.text = "Maaf " + playerName + ", Jawaban kamu masih banyak yang salah! Kamu dapat belajar dengan membaca materi pada buku cacatan.";
            }

            correctAnswersTextLose.text = correctAnswersCount + " dari " + totalQuestions;
            remainingTimeTextLose.text = currentTime + " detik";
            totalScoreTextLose.text = (correctAnswersCount * 2 * currentTime).ToString();
        }
    }


    IEnumerator LoadGame(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

    public void PlayAgain()
    {
        StartCoroutine(LoadGame(Scene));
    }

    public void BackToGameplay()
    {
        SceneManager.UnloadSceneAsync(Scene);
    }

    public void AnswerButtonClicked(int buttonIndex)
    {
        // Memeriksa jawaban yang dipilih oleh pemain
        if (buttonIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            Debug.Log("Jawaban benar!");
            correctAnswersCount++;
        }
        else
        {
            Debug.Log("Jawaban salah!");
        }

        // Pindah ke pertanyaan selanjutnya
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            LoadQuestion(currentQuestionIndex);
        }
        else
        {
            ShowResult();
        }
    }
}
