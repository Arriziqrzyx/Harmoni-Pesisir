using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamplayIntro : MonoBehaviour
{
    public TextMeshProUGUI conversationText;
    public Button nextButton;
    public Image teacherImage;

    public Sprite[] teacherSprites;

    private string[] sentences = {
        "Sekarang kita sudah sampai pada wilayah pelatihan lapangan, [nama]!. Lihatlah, laut ini masih penuh dengan sampah dan rumput laut liar yang membuat ikan sulit bergerak bebas.",
        "Aku akan memberimu tugas. Kerjakan tugas tersebut agar kamu bisa menyelesaikan pelatihan lapangan ini. Tugas yang diberi akan selalu berbeda pada tiap kesempatan. Baca baik-baik tugas tersebut dan kerjakan.",
        "Jika kamu melihat flora dan fauna yang belum pernah kamu lihat sebelumnya, beri tahu padaku dengan cara menunjuknya. Aku akan memberikan informasi mengenai itu.",
        "Jangan lupa untuk selalu mencatatnya dibuku catatan yang sudah aku berikan ya, [nama]!",
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
        SceneManager.LoadScene("GamePlay");
    }

    string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey, "Player");
    }
}
