using UnityEngine;
using TMPro;

public class UkurGameManager : MonoBehaviour
{
    private int skor = 0;
    private int activatedObjectCount = 0;

    [SerializeField] TMP_Text skorText;
    [SerializeField] TMP_Text activatedCountText;

    public GameObject[] objectsToActivate;
    public GameObject panelToShow;
    public GameObject check;
    public float delayToShowPanel = 1.5f;
    public TMP_Text resultMessageText;

    // High score variables
    private int highScoreL3 = 0;
    private string highScoreL3Key = "HighScoreL3";

    void Start()
    {
        // Mendapatkan high score dari PlayerPrefs saat permainan dimulai
        highScoreL3 = PlayerPrefs.GetInt(highScoreL3Key, 0);

        UpdateSkorUI();
        UpdateActivatedCountUI();
    }

    void UpdateSkorUI()
    {
        skorText.text = "Skor: " + skor;
    }

    void UpdateActivatedCountUI()
    {
        activatedCountText.text = "Telah diukur: " + activatedObjectCount + "/" + objectsToActivate.Length;
    }

    public void TambahSkor(int nilai)
    {
        skor = Mathf.Max(0, skor + nilai);
        UpdateSkorUI();

        // Cek jika semua objek telah teraktifkan
        bool allObjectsActivated = CheckAllObjectsActivated();

        if (allObjectsActivated)
        {
            // Memanggil fungsi untuk menampilkan panel dengan delay
            Invoke("ShowPanel", delayToShowPanel);
        }
    }

    bool CheckAllObjectsActivated()
    {
        // Iterasi melalui array untuk memeriksa apakah semua objek telah teraktifkan
        activatedObjectCount = 1;

        foreach (GameObject obj in objectsToActivate)
        {
            if (obj.activeSelf)
            {
                activatedObjectCount++;
            }
        }

        UpdateActivatedCountUI();

        // Jika semua objek telah teraktifkan, kembalikan true
        return activatedObjectCount == objectsToActivate.Length;
    }

    void ShowPanel()
    {
        // Menampilkan objek panel
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
        }

        check.SetActive(true);

        // Membandingkan skor dengan high score dan menyimpan jika skor baru lebih tinggi
        if (skor > highScoreL3)
        {
            highScoreL3 = skor;
            PlayerPrefs.SetInt(highScoreL3Key, highScoreL3);
        }

        // Menampilkan pesan setelah panel ditampilkan
        if (resultMessageText != null)
        {
            resultMessageText.text = "Kereeeen! Kamu telah mengukur semua karang. Skor kamu: " + skor;
        }
    }
}
