using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DIalogPilihSampah : MonoBehaviour
{
    public TextMeshProUGUI conversationText;
    public Button nextButton;
    public Image teacherImage;

    public Sprite[] teacherSprites;

    private string[] sentences = {
        "Kali ini [nama] akan diminta untuk membuang sampah yang tadi sudah [nama] kumpulkan dari wilayah konservasi laut. Sampah adalah sisa buangan dari suatu produk atau barang yang sudah tidak digunakan lagi, tetapi masih dapat di daur ulang menjadi barang yang bernilai.",
        "[nama] harus memisahkan sampah-sampah tersebut kedalam dua jenis, yaitu sampah organik dan sampah anorganik. Apa kamu sudah tau perbedaan sampah organik dan anorganik?",
        "Sampah organik adalah sampah yang berasal dari sisa mahkluk hidup yang mudah terurai secara alami tanpa proses campur tangan manusia untuk dapat terurai.",
        "Sampah organik bisa dikatakan sebagai sampah ramah lingkungan bahkan sampah bisa diolah kembali menjadi suatu yang bermanfaat bila dikelola dengan tepat. Tetapi bila tidak dikelola dengan benar akan menimbulkan penyakit dan bau yang kurang sedap hasil dari pembusukan sampah organik yang cepat.",
        "Sementara itu, sampah anorganik merupakan sampah yang tidak mudah luruh sehingga dapat bertahan dengan waktu yang lama. Ini dapat mencemari lautan, merusak terumbu karang, atau bahkan membunuh hewan",
        "Sebagai contoh sampah organik adalah potongan tubuh makhluk hidup seperti tulang-belulang, buah-buahan, cangkang telur, dan dedaunan",
        "Contoh sampah anorganik adalah olahan plastik, kaleng, kaca, dan logam.",
        "Apakah [nama] sudah paham? Ayo mulai pisahkan sampah-sampah tersebut!"


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
        SceneManager.LoadScene("PilahSampah");
    }

    string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey, "Player");
    }
}
