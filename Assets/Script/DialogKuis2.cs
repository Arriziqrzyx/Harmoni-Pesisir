using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogKuis2 : MonoBehaviour
{
    public TextMeshProUGUI conversationText;
    public Button nextButton;
    public Image teacherImage;
    public GameObject PanelDialog;

    public Sprite[] teacherSprites;

    private string[] sentences = {
        "Halo [nama], aku senang kamu kemari tepat waktu.",
        "Pada minggu ketiga ini, materi yang akan diujikan adalah mengenai pengenalan Terumbu Karang dan cara melakukan konservasi melalui teknik transplantasi.",
        "Sepertinya kemarin kamu sudah mempelajari cara-cara transplantasi pada pelatihan lapangan, jadi aku harap kamu dapat menjawab semua pertanyaan dengan benar.",
        "Sebelum memulai, kamu dapat belajar dengan membaca buku catatan yang aku berikan. Terdapat 5 soal pilihan ganda dengan batas waktu 50 detik",
        "Jawablah dengan bersungguh-sungguh dan secepat mungkin agar kamu mendapat skor yang besar!",
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
            PanelDialog.SetActive(false);
        }
    }

    string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey, "Player");
    }
}
