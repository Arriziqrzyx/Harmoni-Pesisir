using UnityEngine;

public class TrashType : MonoBehaviour
{
    public bool isOrganic;
    private bool isBeingDragged = false;
    private GameObject organicBin;
    private GameObject inorganicBin;
    private TrashBinManager trashBinManager;
    private Vector2 spawnPosition;

    void Start()
    {
        // Nonaktifkan collider selama 1 detik pada awal permainan
        DisableColliderForOneSecond();
    }

    void DisableColliderForOneSecond()
    {
        // Nonaktifkan collider
        GetComponent<Collider2D>().enabled = false;

        // Aktifkan kembali setelah 1 detik
        Invoke("EnableCollider", 1f);
    }

    void EnableCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void SetBins(GameObject organicBin, GameObject inorganicBin, TrashBinManager trashBinManager)
    {
        this.organicBin = organicBin;
        this.inorganicBin = inorganicBin;
        this.trashBinManager = trashBinManager;
        spawnPosition = transform.position;
    }

    void Update()
    {
        if (isBeingDragged)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }

    void OnMouseDown()
    {
        isBeingDragged = true;
    }

    void OnMouseUp()
    {
        isBeingDragged = false;
        CheckDropZone();
    }

    private void ResetToStartPosition()
    {
        transform.position = spawnPosition;
    }

    private void CheckDropZone()
    {
        bool isInsideBinArea = IsInsideBinArea();

        if (isInsideBinArea)
        {
            bool isCorrectBin = false;

            if (isOrganic && inorganicBin.GetComponent<Collider2D>().OverlapPoint(transform.position))
            {
                isCorrectBin = false;
            }
            else if (!isOrganic && organicBin.GetComponent<Collider2D>().OverlapPoint(transform.position))
            {
                isCorrectBin = false;
            }
            else
            {
                isCorrectBin = true;
            }

            if (isCorrectBin)
            {
                trashBinManager.TrashEnteredBin(true); // Memasukkan jenis sampah benar
                Destroy(gameObject);
            }
            else
            {
                trashBinManager.TrashEnteredBin(false); // Memasukkan jenis sampah salah
                Destroy(gameObject);
            }
        }
        else
        {
            // Tidak ada perubahan skor jika tidak dimasukkan ke tong sampah
            ResetToStartPosition();
        }
    }

    private bool IsInsideBinArea()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        foreach (Collider2D collider in colliders)
        {
            GameObject dropZone = collider.gameObject;

            if (dropZone == organicBin || dropZone == inorganicBin)
            {
                return true;
            }
        }

        return false;
    }
}
