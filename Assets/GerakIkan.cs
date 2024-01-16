using UnityEngine;
using DG.Tweening;

public class GerakIkan : MonoBehaviour
{
    public float kecepatanIkan = 5f;
    public float batasHancur = 13f;
    public float jarakNaikTurun = 2f;
    public GameObject popup;

    private bool sedangNaikTurun = false;
    private bool isGamePaused = false;
    public bool IsGamePaused { get { return isGamePaused; } }

    void Start()
    {
        // Tentukan arah pergerakan
        kecepatanIkan = (transform.position.x < 0) ? Mathf.Abs(kecepatanIkan) : -Mathf.Abs(kecepatanIkan);

        // Tentukan flip sprite berdasarkan posisi spawn
        GetComponent<SpriteRenderer>().flipX = (kecepatanIkan < 0);

        // Mulai pergerakan naik turun
        StartNaikTurun();
    }

    void Update()
    {
        if (!isGamePaused)
        {
            // Bergerak sesuai arah yang telah ditentukan
            transform.Translate(Vector2.right * kecepatanIkan * Time.deltaTime);

            // Hancurkan ikan jika mencapai batas tertentu
            if (transform.position.x >= batasHancur || transform.position.x <= -batasHancur)
            {
                HandleIkanMelewatiBatas();
            }
        }
    }

    void OnMouseDown()
    {
        if (!isGamePaused)
        {
            // Menampilkan popup jika permainan tidak sedang di-pause
            popup.SetActive(true);

            // Menghentikan pergerakan ikan dan mem-pause permainan
            PauseGame();
        }
    }

    void HandleIkanMelewatiBatas()
    {
        // Hancurkan objek
        Destroy(gameObject);
    }

    void StartNaikTurun()
    {
        if (!sedangNaikTurun)
        {
            sedangNaikTurun = true;
            float targetY = transform.position.y + jarakNaikTurun;

            // DOTween untuk pergerakan naik turun
            transform.DOMoveY(targetY, 5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutBack);
        }
    }

    public void ResumeGame()
    {
        // Memulai kembali pergerakan ikan setelah menutup popup
        isGamePaused = false;
    }

    void PauseGame()
    {
        // Menghentikan pergerakan ikan dan mem-pause permainan
        isGamePaused = true;
    }
}
