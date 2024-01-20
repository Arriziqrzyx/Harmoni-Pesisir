using UnityEngine;
using TMPro;

public class HitungKarangTerpasang : MonoBehaviour
{
    public Transform parentObjek;
    public TMP_Text textKarangTerpasang;
    public GameObject CheckSukses; // Tambahkan variabel panelWin untuk merujuk ke panel kemenangan
    public GameObject panelWin; // Tambahkan variabel panelWin untuk merujuk ke panel kemenangan
    public TMP_Text resultMessageText;
    private float delayBeforeWin = 1f; // Tambahkan delay sebelum menampilkan panel kemenangan

    private int jumlahChildrenAwal;

    void Start()
    {
        // Mengambil jumlah children awal pada saat inisialisasi
        jumlahChildrenAwal = parentObjek.childCount;

        // Memperbarui teks pada TMP_Text
        UpdateTextKarangTerpasang();
    }

    void UpdateTextKarangTerpasang()
    {
        // Menghitung jumlah children yang tersisa
        int jumlahChildrenSisa = parentObjek.childCount;

        // Menghitung jumlah children yang telah terhapus
        int jumlahChildrenTerhapus = jumlahChildrenAwal - jumlahChildrenSisa;

        // Memperbarui teks pada TMP_Text
        textKarangTerpasang.text = "Karang Terpasang " + jumlahChildrenTerhapus + "/" + jumlahChildrenAwal;

        // Jika semua children telah terhapus, aktifkan panelWin setelah delay
        if (jumlahChildrenSisa == 0)
        {
            Invoke("AktifkanPanelWin", delayBeforeWin);
        }
    }

    void Update()
    {
        // Memanggil fungsi UpdateTextKarangTerpasang setiap frame
        UpdateTextKarangTerpasang();
    }

    void AktifkanPanelWin()
    {
        // Aktifkan panel win
        CheckSukses.SetActive(true);
        panelWin.SetActive(true);

        if (resultMessageText != null)
        {
            resultMessageText.text = "Kereeeen! Kamu telah memasang semua karang:";
        }
    }

    public void ChildDestroyed()
    {
        // Dipanggil ketika children objek dihapus
        // Mengupdate teks setelah ada perubahan pada children
        UpdateTextKarangTerpasang();
    }
}
