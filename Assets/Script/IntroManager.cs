using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI emptyText;
    public TMP_InputField nameInputField;
    public GameObject submitButton;

    private string playerNameKey = "PlayerName";

    private void Start()
    {
        // PlayerPrefs.DeleteKey(playerNameKey);
        // Mengecek apakah nama pemain sudah diisi sebelumnya
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            // Jika sudah, langsung pindah ke scene "Menu"
            Debug.Log("Sudah isi nama sebelumnya");
            SceneManager.LoadScene("Menu");
        }
        else
        {
            // Jika belum, jalankan proses normal
            Debug.Log("belum isi nama");
            submitButton.gameObject.SetActive(false);
            nameInputField.gameObject.SetActive(false);
            emptyText.gameObject.SetActive(false);
            questionText.text = "";
            StartCoroutine(ShowQuestionText());
        }
    }

    private IEnumerator ShowQuestionText()
    {
        const string question = "Sebelum mulai, Beritahu siapa nama kamu penjelajah kecil?";
        foreach (char letter in question.ToCharArray())
        {
            questionText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }

        // Menunggu sejenak sebelum menampilkan InputField
        yield return new WaitForSeconds(0.5f);

        nameInputField.gameObject.SetActive(true);
        StartCoroutine(WaitForTypingAnimation());
    }

    private IEnumerator WaitForTypingAnimation()
    {
        // Menunggu hingga animasi mengetik pada InputField selesai
        while (nameInputField.isFocused && nameInputField.caretPosition < nameInputField.text.Length)
        {
            yield return null;
        }

        // Setelah animasi selesai, aktifkan tombol submit
        submitButton.SetActive(true);
    }

    public void SubmitName()
    {
        string playerName = nameInputField.text;

        // Memeriksa apakah input nama tidak kosong
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            PlayerPrefs.SetString(playerNameKey, playerName);
            PlayerPrefs.Save();
            Debug.Log("Nama disimpan: " + playerName);

            // Menonaktifkan Panel 1 dan Mengaktifkan Panel 2
            SceneManager.LoadScene("Intro2");
        }
        else
        {
            Debug.LogWarning("Input nama tidak boleh kosong!");
            emptyText.gameObject.SetActive(true);
            // Anda dapat menambahkan logika atau pesan lain sesuai kebutuhan
        }
    }
}
