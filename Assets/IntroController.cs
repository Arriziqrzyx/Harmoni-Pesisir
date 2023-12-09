using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import namespace untuk menggunakan SceneManager

public class IntroController : MonoBehaviour
{
    public TextMeshProUGUI conversationText;
    public Button nextButton;

    private string[] sentences = {
        "Selamat datang di game konservasi laut kami! Kami sangat senang Anda bergabung untuk memahami pentingnya menjaga kelestarian ekosistem laut dan pantai.",
        "Dalam permainan ini, Anda akan mengeksplorasi kehidupan bawah laut, memahami tantangan yang dihadapi oleh makhluk-makhluk laut, dan belajar bagaimana kita dapat berkontribusi untuk melindungi lautan.",
        "Jangan lupa untuk selalu mendukung kampanye pelestarian lingkungan laut. Setiap tindakan kecil yang kita lakukan dapat memiliki dampak besar untuk keberlanjutan ekosistem laut.",
        "Laut adalah rumah bagi berbagai jenis organisme, mulai dari ikan kecil hingga paus raksasa. Melalui permainan ini, Anda akan menjadi agen pelestarian yang bertanggung jawab menjaga keseimbangan ekosistem laut.",
        "Pentingnya menjaga kebersihan laut dan pantai tidak hanya berdampak pada kehidupan laut, tetapi juga pada kesejahteraan manusia. Perairan yang bersih memberikan kehidupan pada masyarakat pesisir dan menciptakan lingkungan yang sehat untuk semua.",
        "Semoga permainan ini memberikan pengalaman yang mendalam dan memicu kesadaran kita tentang urgensi menjaga lautan yang menjadi sumber kehidupan bagi banyak makhluk di Bumi.",
        "Terima kasih telah bergabung dalam petualangan konservasi laut ini. Mari bersama-sama menjaga dan merawat keindahan alam bawah laut untuk generasi yang akan datang."
    };

    private int currentSentenceIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        nextButton.onClick.AddListener(NextSentence);
        StartCoroutine(ShowConversation());
    }

    IEnumerator ShowConversation()
    {
        isTyping = true;
        foreach (char letter in sentences[currentSentenceIndex].ToCharArray())
        {
            conversationText.text += letter;
            yield return new WaitForSeconds(0.06f);
        }
        isTyping = false;
    }

    void NextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            conversationText.text = sentences[currentSentenceIndex];
            isTyping = false;
        }
        else if (currentSentenceIndex < sentences.Length - 1)
        {
            currentSentenceIndex++;
            conversationText.text = "";
            StartCoroutine(ShowConversation());
        }
        else
        {
            Debug.Log("Percakapan selesai.");
            LoadMenuScene(); // Panggil fungsi untuk beralih ke scene "Menu"
        }
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu"); // Ganti "Menu" dengan nama scene sesuai kebutuhan Anda
    }
}
