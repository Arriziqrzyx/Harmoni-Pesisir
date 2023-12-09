using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ImageButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    private bool isButtonPressed = false;
    private bool isPointerInside = false;
    private Vector3 originalScale;
    public GameObject panelToActivate; // Panel yang akan diaktifkan

    void Start()
    {
        // Simpan skala asli objek saat mulai
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isButtonPressed && isPointerInside)
        {
            // Tambahkan logika di sini untuk aksi yang ingin Anda lakukan selama tombol ditekan
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;

        // Menambahkan efek visual dengan DOTween (perubahan skala saat tombol ditekan)
        transform.DOScale(originalScale * 1.2f, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;

        // Menambahkan efek visual dengan DOTween (perubahan skala saat tombol dilepas)
        transform.DOScale(originalScale, 0.2f).OnComplete(() =>
        {
            // Hanya menjalankan fungsi jika tombol dilepas di dalam area tombol
            if (isPointerInside)
            {
                Debug.Log("Fungsi tombol dijalankan!");
                // Tambahkan fungsi yang ingin Anda jalankan di sini
                if (panelToActivate != null)
                {
                    panelToActivate.SetActive(true);
                }
            }
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;
        isButtonPressed = false;

        // Mengembalikan skala ke skala asli jika keluar dari area tombol saat tombol masih ditekan
        transform.DOScale(originalScale, 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;
    }
}
