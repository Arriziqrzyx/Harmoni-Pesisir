using UnityEngine;

public class SampahSpawner : MonoBehaviour
{
    public GameObject[] sampahPrefabs; // Array untuk menyimpan prefab sampah
    public float spawnHeight = 1.0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnSampah();
        }
    }

    void SpawnSampah()
    {
        // Memilih prefab sampah secara acak dari array sampahPrefabs
        GameObject selectedSampahPrefab = sampahPrefabs[Random.Range(0, sampahPrefabs.Length)];

        // Menentukan posisi spawn secara acak di sekitar tempat sampah
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-1.0f, 1.0f),
            spawnHeight,
            transform.position.z + Random.Range(-1.0f, 1.0f)
        );

        // Meng-instantiate prefab sampah yang dipilih dengan posisi, rotasi, dan nilai bool yang sudah ditentukan
        GameObject spawnedSampah = Instantiate(selectedSampahPrefab, spawnPosition, Quaternion.identity);
        SampahController sampahController = spawnedSampah.GetComponent<SampahController>();
        if (sampahController != null)
        {
            // Sesuaikan properti lainnya berdasarkan jenis sampah jika diperlukan
        }
    }
}
