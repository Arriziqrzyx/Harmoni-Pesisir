using UnityEngine;

public class IkanGameManager : MonoBehaviour
{
    public GameObject[] ikanPrefabs;
    public Transform spawnPointKiri;
    public Transform spawnPointKanan;

    void Start()
    {
        // Panggil fungsi SpawnIkan setiap beberapa detik (misalnya, setiap 8 detik)
        InvokeRepeating("SpawnIkan", 0f, 8f);
    }

    void SpawnIkan()
    {
        // Tentukan posisi spawn secara acak antara kiri dan kanan
        Transform spawnPoint;
        if (Random.Range(0, 2) == 0)
        {
            spawnPoint = spawnPointKiri;
        }
        else
        {
            spawnPoint = spawnPointKanan;
        }

        // Tentukan prefab ikan secara acak dari array ikanPrefabs
        GameObject ikanPrefab = ikanPrefabs[Random.Range(0, ikanPrefabs.Length)];

        // Instantiate prefab ikan di spawnPoint
        GameObject ikan = Instantiate(ikanPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void stopIkan()
    {
        // Hentikan spawing
        CancelInvoke("SpawnIkan");

        // Nonaktifkan script GerakIkan pada semua ikan yang ada
        GerakIkan[] ikanArray = FindObjectsOfType<GerakIkan>();
        foreach (GerakIkan ikan in ikanArray)
        {
            ikan.enabled = false;
        }
    }
}
