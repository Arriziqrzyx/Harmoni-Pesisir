using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroManager2 : MonoBehaviour
{
    public TextMeshProUGUI conversationText;
    public Button nextButton;
    public Image teacherImage;

    public Sprite[] teacherSprites;

    private string[] sentences = {
        "Selamat datang di lokasi konservasi laut kami! Aku Tantan, temanmu yang akan membantumu belajar disini. Aku sangat senang [nama] bergabung untuk memahami pentingnya menjaga kelestarian ekosistem laut dan pantai.",
        "[nama] akan mengeksplorasi kehidupan bawah laut, memahami tantangan yang dihadapi oleh makhluk-makhluk laut, dan belajar bagaimana kita dapat berkontribusi untuk melindungi lautan.",
        "Jangan lupa untuk selalu mendukung kampanye pelestarian lingkungan laut. Setiap tindakan kecil yang [nama] lakukan dapat memiliki dampak besar untuk keberlanjutan ekosistem laut.",
        "Laut adalah rumah bagi berbagai jenis organisme, mulai dari ikan kecil hingga paus raksasa. Melalui pembelajaran ini, [nama] akan menjadi agen pelestarian yang bertanggung jawab menjaga keseimbangan ekosistem laut.",
        "Pentingnya menjaga kebersihan laut dan pantai tidak hanya berdampak pada kehidupan laut, tetapi juga pada kesejahteraan [nama]. Perairan yang bersih memberikan kehidupan pada masyarakat pesisir dan menciptakan lingkungan yang sehat untuk semua.",
        "Semoga pembelajaran ini memberikan pengalaman yang mendalam dan memicu kesadaran kita tentang urgensi menjaga lautan yang menjadi sumber kehidupan bagi banyak makhluk di Bumi.",
        "Terima kasih [nama] telah bergabung dalam petualangan konservasi laut ini. Mari bersama-sama menjaga dan merawat keindahan alam bawah laut untuk generasi yang akan datang.",
        "Pertama-tama, aku akan memberikan [nama] kalender dan buku catatan. Kamu dapat melihat jadwal belajarmu dalam kalender dan menulis pengetahuan yang kamu dapatkan ke dalam catatan",
        "Kita akan mengadakan pembelajaran selama sebulan dengan melakukan pelatihan dan lapangan. Dalam pelatihan, [nama] diminta untuk mengisi kuis untuk menguji pemahaman [nama]. Berusahalah untuk mendapat skor terbaik",
        "Sementara itu, dalam pelatihan kita akan pergi ke lokasi konservasi bersama-sama untuk ikut bertugas bersama petugas konservasi lainnya. ",
        "Jangan lupa catat informasi penting mengenai flora dan fauna yang kamu temui ya!",
        "Ini pasti sangat seru! Jika kamu sudah siap, temui aku di sampan. Kita pergi ke tempat pelatihan bersama-sama!!!"
    };

    private int currentSentenceIndex = 0;
    private bool isTyping = false;

    private string playerNameKey = "PlayerName";

    private void Start()
    {
        nextButton.onClick.AddListener(NextSentence);
        StartCoroutine(ShowConversation());
    }

    IEnumerator ShowConversation()
    {
        isTyping = true;

        if (currentSentenceIndex < teacherSprites.Length)
        {
            teacherImage.sprite = teacherSprites[currentSentenceIndex];
        }

        string sentenceToDisplay = sentences[currentSentenceIndex].Replace("[nama]", GetPlayerName());
        
        foreach (char letter in sentenceToDisplay.ToCharArray())
        {
            conversationText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
    }

    void NextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            conversationText.text = sentences[currentSentenceIndex].Replace("[nama]", GetPlayerName());
            isTyping = false;
        }
        else if (currentSentenceIndex < sentences.Length - 1)
        {
            currentSentenceIndex++;
            conversationText.text = "";

            if (currentSentenceIndex < teacherSprites.Length)
            {
                teacherImage.sprite = teacherSprites[currentSentenceIndex];
            }

            StartCoroutine(ShowConversation());
        }
        else
        {
            Debug.Log("Percakapan selesai.");
            LoadMenuScene();
        }
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey, "Player");
    }
}
