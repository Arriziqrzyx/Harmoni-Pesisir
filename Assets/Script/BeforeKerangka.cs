using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeforeKerangka : MonoBehaviour
{
    public TextMeshProUGUI conversationText;
    public Button nextButton;
    public Image teacherImage;

    public Sprite[] teacherSprites;

    private string[] sentences = {
        "Setelah mengumpulkan karang, kita akan memasang potongan karang tersebut agar bisa tumbuh pada lokasi baru dan membentuk ekosistem baru pula",
        "Namun, kita harus membuat media untuk menempelkan potongan-potongan karang tersebut. Media ini bernama rangka laba-laba. Rangka ini berbentuk segi enam dengan bahan logam yang kokoh.",
        "Caranya seperti menyusun kaki-kaki laba-laba agar saling terhubung dan kuat. Kita gunakan tali kabel besar untuk mengikatnya.",
        "Untuk membuat sistem ini lebih kuat dan aman, kita perlu memastikan bahwa semua bagian laba-laba terhubung dengan baik. Penyusunan terbaik adalah ketika semua kaki laba-laba saling bersilangan dan diikat dengan erat.",
        "Hal ini memastikan bahwa laba-laba tidak terangkat oleh arus atau gelombang laut saat air bergerak melalui struktur laba-laba. Jadi, dengan cara ini, sistem akan lebih kokoh dan dapat menahan tekanan dari lingkungan sekitarnya.",
        "Ayo kita mulai dengan merakit kerangka, dilanjutkan dengan menempelkan karang pada rangka yang telah terakit, dan diakhiri dengan meletakan hasil rakitan semua jenis karang kedalam laut",
        "Baiklah, ayo kita mulai!!"

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
        SceneManager.LoadScene("Kerangka");
    }

    string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey, "Player");
    }
}
