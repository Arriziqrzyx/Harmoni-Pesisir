using UnityEngine;
using DG.Tweening;

public class SpawnKarang : MonoBehaviour
{
    public GameObject potonganKarangPrefab; // Assign prefab potongan karang melalui Inspector
    public Transform keranjangTransform; // Assign objek keranjang melalui Inspector
    public GameObject aktivasiObjek; // Assign objek yang akan diaktifkan setelah spawn 6 kali melalui Inspector
    public AudioSource SoundMetikKarang;

    private bool isMousePressed = false;
    private bool isMouseOver = false;
    private bool canSpawn = true; // Menandakan apakah tombol dapat ditekan lagi
    private int spawnCount = 0;
    private Vector3 originalScale;

    private void Start()
    {
        // Simpan skala asli objek saat mulai
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isMousePressed && isMouseOver && canSpawn)
        {
            // Tambahkan logika di sini untuk aksi yang ingin Anda lakukan selama tombol ditekan
        }
    }

    private void OnMouseDown()
    {
        if (canSpawn)
        {
            isMousePressed = true;

            // Menambahkan efek visual (perubahan skala saat tombol ditekan)
            transform.DOScale(originalScale * 1.009f, 0.2f).SetEase(Ease.OutBack);

            SpawnPotonganKarang();

            // Setel jeda sebelum tombol dapat ditekan lagi
            StartCoroutine(EnableSpawnAfterDelay(0.5f));
        }
    }

    private void OnMouseUp()
    {
        isMousePressed = false;

        // Mengembalikan skala ke skala asli saat tombol dilepas
        transform.DOScale(originalScale, 0.2f);
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }

    private void SpawnPotonganKarang()
    {
        if (spawnCount < 6) // Jika belum mencapai spawn maksimal (6)
        {
            SoundMetikKarang.Play();
            GameObject spawnedKarang = Instantiate(potonganKarangPrefab, transform.position, Quaternion.identity);
            spawnedKarang.GetComponent<PotonganKarang>().SetTargetKeranjang(keranjangTransform);
            spawnCount++;

            // Aktifkan objek setelah spawn 6 kali
            if (spawnCount == 6 && aktivasiObjek != null)
            {
                aktivasiObjek.SetActive(true);
            }
        }
    }

    private System.Collections.IEnumerator EnableSpawnAfterDelay(float delay)
    {
        canSpawn = false;
        yield return new WaitForSeconds(delay);
        canSpawn = true;
    }
}
