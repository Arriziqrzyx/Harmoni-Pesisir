using UnityEngine;
using TMPro;

public class UkurGameManager : MonoBehaviour
{
    private int skor = 0;
    [SerializeField] TMP_Text skorText;
    [SerializeField] TMP_Text activatedCountText;

    public GameObject[] objectsToActivate; // Objek yang akan diaktifkan
    public GameObject panelToShow; // Objek panel yang akan ditampilkan
    public GameObject check; // Objek panel yang akan ditampilkan
    public float delayToShowPanel = 1.5f; // Delay sebelum menampilkan panel

    private int activatedObjectCount = 0;

    void Start()
    {
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
    }
}
