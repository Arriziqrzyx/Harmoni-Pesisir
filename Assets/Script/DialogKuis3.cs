using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogKuis3 : MonoBehaviour
{
    public TextMeshProUGUI conversationText;
    public Button nextButton;
    public Image teacherImage;
    public GameObject PanelDialog;

    public Sprite[] teacherSprites;

    private string[] sentences = {
        "Halo [nama], ini adalah ujian terakhir kita. Tidak terasa ya...",
        "Oke, pada ujian kali ini materi yang akan diuji adalah mengenai kehidupan ikan karang dan jenisnya",
        "jangan lupa untuk belajar pada buku yang aku berikan, kamu bisa membaca materi ini pada tab bergambar ikan.",
        "baiklah, ayo kita mulai!!"

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
            PanelDialog.SetActive(false);
        }
    }

    string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey, "Player");
    }
}
