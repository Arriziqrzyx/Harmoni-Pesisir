using UnityEngine;
using TMPro;

public class DisplayScoreU4 : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Start()
    {
        // Mengambil nilai high score dari PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScoreU4", 0);
        
        // Menetapkan nilai high score ke teks pada objek
        if (scoreText != null)
        {
            scoreText.text = highScore.ToString();
        }
    }
}
