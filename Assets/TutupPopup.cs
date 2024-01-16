using UnityEngine;

// ...

public class TutupPopup : MonoBehaviour
{
    public GameObject pop;
    private bool isButtonPressed = false;

    void Update()
    {
        if (isButtonPressed)
        {
            // Tambahkan logika di sini untuk aksi yang ingin Anda lakukan selama tombol ditekan
        }
    }

    void OnMouseDown()
    {
        isButtonPressed = true;

        // Menutup popup dan memulai kembali permainan
        pop.SetActive(false);

        // Mendapatkan skrip GerakIkan dari objek yang ada di scene
        GerakIkan gerakIkan = pop.GetComponentInParent<GerakIkan>();

        // Periksa apakah objek memiliki skrip GerakIkan
        if (gerakIkan != null)
        {
            // Memulai kembali pergerakan ikan setelah menutup popup
            gerakIkan.ResumeGame();
        }
        else
        {
            Debug.LogWarning("GerakIkan not found");
        }
    }
}
