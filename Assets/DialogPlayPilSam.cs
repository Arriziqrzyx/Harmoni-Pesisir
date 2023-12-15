using UnityEngine;
using TMPro;
using System.Collections;

public class DialogPlayPilSam : MonoBehaviour
{
    private bool isMessagePlaying = false;
    public TextMeshProUGUI messageText;

    public IEnumerator TypeMessage(string message, bool isCorrect)
    {
        messageText.text = "";

        foreach (char letter in message)
        {
            messageText.text += letter;
            yield return new WaitForSeconds(0.025f); // Ubah nilai ini untuk mengatur kecepatan ketik
        }
    }

    public void SetIsMessagePlaying(bool value)
    {
        isMessagePlaying = value;
    }

}
