using UnityEngine;
using TMPro;

public class KerangkaManager : MonoBehaviour
{
    [Header("Bagian Pertama")]
    public TextMeshProUGUI infoTextGaris;
    public TextMeshProUGUI infoTextTali;
    public TextMeshProUGUI timerText;
    public GameObject checkImageGaris;
    public GameObject checkImageTali;
    public GameObject taliGroup;

    [Header("Bagian Kedua")]
    public TextMeshProUGUI infoTextKarang;
    public GameObject checkImageKarang1;
    public GameObject checkImageKarang2;
    public GameObject checkImageKarang3;
    public GameObject checkImageKarang4;
    public GameObject checkImageKarang5;
    public GameObject checkImageKarang6;
    public GameObject checkImageFinish;

    public GameObject dragKarang;
    public GameObject dragKarang2;
    public GameObject dragKarang3;
    public GameObject dragKarang4;
    public GameObject dragKarang5;
    public GameObject dragKarang6;

    public GameObject dropKarang;
    public GameObject dropKarang2;
    public GameObject dropKarang3;
    public GameObject dropKarang4;
    public GameObject dropKarang5;
    public GameObject dropKarang6;

    [Header("Game Over Panel")]
    public GameObject winPanel;
    public TextMeshProUGUI sisaWaktu;
    public TextMeshProUGUI scoreResult;

    public GameObject losePanel;
    public TextMeshProUGUI sisaWaktuLose;
    public TextMeshProUGUI scoreLose;

    [Header("Aktivasi & Nonaktifkan")]
    public GameObject pesan;
    public GameObject pesanBaru;
    public GameObject pesanBaru2;
    public GameObject papanLama;
    public GameObject papanBaru;

    private int dropAreaCountGaris = 0;
    private int totalDropAreaCountGaris;

    private int dropAreaCountTali = 0;
    private int totalDropAreaCountTali;

    private float timerDuration = 45f;
    private float timer;
    private int currentScore;

    private bool isGameOver = false;

    void Start()
    {
        totalDropAreaCountGaris = GameObject.FindGameObjectsWithTag("DropAreaGaris").Length;
        totalDropAreaCountTali = CountActiveTaliObjects();

        if (taliGroup != null)
        {
            taliGroup.SetActive(false);
        }

        UpdateInfoText();
        timer = timerDuration;
    }

    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = 0;
                Kalah();
            }

            if (timer >= 0 && timer <= timerDuration)
            {
                if (timerText != null)
                {
                    timerText.text = "Sisa Waktu: " + Mathf.CeilToInt(timer);
                }
            }
        }
    }

    public void UpdateInfoText()
    {
        if (infoTextGaris != null)
        {
            infoTextGaris.text = "Tongkat terpasang " + dropAreaCountGaris + "/" + totalDropAreaCountGaris;
        }

        if (infoTextTali != null)
        {
            infoTextTali.text = "Tali terpasang " + dropAreaCountTali + "/" + totalDropAreaCountTali;
        }

        if (infoTextKarang != null)
        {
            infoTextKarang.text = "Semua karang telah terpasang";
        }

        if (dropAreaCountGaris == totalDropAreaCountGaris && !checkImageGaris.activeSelf)
        {
            if (checkImageGaris != null)
            {
                checkImageGaris.SetActive(true);
            }

            if (taliGroup != null)
            {
                taliGroup.SetActive(true);
            }
        }

        if (dropAreaCountTali == totalDropAreaCountTali && !checkImageTali.activeSelf)
        {
            if (checkImageTali != null)
            {
                checkImageTali.SetActive(true);

                Invoke("DelayedObjectiveActions", 0.5f);
            }
        }
    }

    public void IncrementDropAreaCountGaris()
    {
        dropAreaCountGaris++;
        UpdateInfoText();
    }

    void DelayedObjectiveActions()
    {
        Destroy(pesan);

        if (pesanBaru != null)
        {
            pesanBaru.SetActive(true);
        }

        if (papanBaru != null)
        {
            papanBaru.SetActive(true);
        }

        if (dragKarang != null)
        {
            dragKarang.SetActive(true);
        }

        if (dropKarang != null)
        {
            dropKarang.SetActive(true);
        }
    }

    void DelayedObjectiveActions2()
    {
        Destroy(dropKarang);

        if (dragKarang2 != null)
        {
            dragKarang2.SetActive(true);
        }

        if (dropKarang2 != null)
        {
            dropKarang2.SetActive(true);
        }

        Destroy(pesanBaru);

        if (pesanBaru2 != null)
        {
            pesanBaru2.SetActive(true);
        }
    }

    void DelayedObjectiveActions3()
    {
        Destroy(dropKarang2);

        if (dragKarang3 != null)
        {
            dragKarang3.SetActive(true);
        }

        if (dropKarang3 != null)
        {
            dropKarang3.SetActive(true);
        }
    }

    void DelayedObjectiveActions4()
    {
        Destroy(dropKarang3);

        if (dragKarang4 != null)
        {
            dragKarang4.SetActive(true);
        }

        if (dropKarang4 != null)
        {
            dropKarang4.SetActive(true);
        }
    }

    void DelayedObjectiveActions5()
    {
        Destroy(dropKarang4);

        if (dragKarang5 != null)
        {
            dragKarang5.SetActive(true);
        }

        if (dropKarang5 != null)
        {
            dropKarang5.SetActive(true);
        }
    }

    void DelayedObjectiveActions6()
    {
        Destroy(dropKarang5);

        if (dragKarang6 != null)
        {
            dragKarang6.SetActive(true);
        }

        if (dropKarang6 != null)
        {
            dropKarang6.SetActive(true);
        }
    }

    void DelayedObjectiveActions7()
    {
        Destroy(pesanBaru2);

        if (checkImageFinish != null)
        {
            checkImageFinish.SetActive(true);
        }

        CalculateScore();
        Menang();
        isGameOver = true;
    }

    public void IncrementDropAreaCountTali()
    {
        dropAreaCountTali++;
        UpdateInfoText();
    }

    int CountActiveTaliObjects()
    {
        Tali[] taliObjects = FindObjectsOfType<Tali>();
        int count = 0;

        foreach (Tali tali in taliObjects)
        {
            if (tali.gameObject.activeSelf)
            {
                count++;
            }
        }

        return count;
    }

    void CalculateScore()
    {
        float timeRemaining = timerDuration - timer;

        if (timeRemaining <= 20f)
        {
            currentScore = 100;
        }
        else if (timeRemaining <= 25f)
        {
            currentScore = 80;
        }
        else if (timeRemaining <= 30f)
        {
            currentScore = 70;
        }
        else
        {
            currentScore = 0;
        }
    }

    void Kalah()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }

        if (sisaWaktuLose != null)
        {
            sisaWaktuLose.text = "0 Detik";
        }

        if (scoreLose != null)
        {
            scoreLose.text = "0";
        }
    }

    void Menang()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

       if (sisaWaktu != null)
        {
            sisaWaktu.text = Mathf.CeilToInt(timer) + " Detik";
        }

        if (scoreResult != null)
        {
             scoreResult.text = currentScore.ToString();
        }
    }
}
