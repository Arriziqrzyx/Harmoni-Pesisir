using UnityEngine;
using DG.Tweening;

public class ButtonTerumbuBaru : MonoBehaviour
{
    private bool isMousePressed = false;
    private bool isMouseOver = false;
    [SerializeField] GameObject aktifkanObjek;
    private Vector3 originalScale;

    private bool isScriptActive = true; // Tambahkan variabel untuk menentukan apakah skrip aktif atau tidak

    void Start()
    {
        // Simpan skala asli objek saat mulai
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isScriptActive && isMousePressed && isMouseOver)
        {
            // Tambahkan logika di sini untuk aksi yang ingin Anda lakukan selama tombol ditekan
        }
    }

    private void OnMouseDown()
    {
        if (isScriptActive)
        {
            isMousePressed = true;

            // Menambahkan efek visual dengan DOTween (perubahan skala saat tombol ditekan)
            transform.DOScale(originalScale * 1.1f, 0.1f).SetEase(Ease.OutBack);
        }
    }

    private void OnMouseUp()
    {
        if (isScriptActive)
        {
            isMousePressed = false;

            // Menambahkan efek visual dengan DOTween (perubahan skala saat tombol dilepas)
            transform.DOScale(originalScale, 0.1f).OnComplete(() =>
            {
                // Hanya menjalankan fungsi jika tombol dilepas di dalam area tombol
                if (isMouseOver)
                {
                    if (aktifkanObjek != null)
                    {
                        aktifkanObjek.SetActive(true);
                    }
                }
            });
        }
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        isMousePressed = false;

        // Mengembalikan skala ke skala asli jika keluar dari area tombol saat tombol masih ditekan
        transform.DOScale(originalScale, 0.1f);
    }

    private void OnMouseEnter()
    {
        if (isScriptActive)
        {
            isMouseOver = true;
        }
    }

    // Fungsi untuk menonaktifkan skrip
    public void NonaktifkanSkrip()
    {
        isScriptActive = false;
    }
}
