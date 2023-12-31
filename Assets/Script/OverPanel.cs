using UnityEngine;

public class OverPanel : MonoBehaviour
{
    public GameObject[] aktivasiObjekArray = new GameObject[6]; // Assign objek yang diaktifkan oleh SpawnKarang melalui Inspector
    public GameObject checkObject; // Assign objek "check" melalui Inspector
    public GameObject panelToActivate; // Assign panel yang akan diaktifkan jika semua objek sudah aktif melalui Inspector

    private bool isCheckObjectActivated = false;

    private void Update()
    {
        // Cek apakah semua aktivasiObjek diaktifkan
        if (CheckAllAktivasiObjekActivated() && !isCheckObjectActivated)
        {
            // Aktifkan objek "check" setelah aktivasiObjekArray terpenuhi
            if (checkObject != null)
            {
                checkObject.SetActive(true);
                isCheckObjectActivated = true;

                // Tunggu 1.5 detik sebelum mengaktifkan panel
                StartCoroutine(ActivatePanelAfterDelay(1.5f));
            }
        }
    }

    private bool CheckAllAktivasiObjekActivated()
    {
        // Cek apakah semua objek diaktifkan
        foreach (GameObject obj in aktivasiObjekArray)
        {
            if (obj != null && !obj.activeSelf)
            {
                return false; // Jika ada objek yang belum aktif, kembalikan false
            }
        }

        return true; // Semua objek sudah aktif
    }

    private System.Collections.IEnumerator ActivatePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Aktifkan panel setelah menunggu 1.5 detik
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
    }
}
