using UnityEngine;
using DG.Tweening;
using UnityEngine.Jobs;

public class TrashController : MonoBehaviour
{
    public float floatSpeed = 1f; // Kecepatan terombang-ambing
    public float floatDistance = 0.5f; // Jarak maksimum terombang-ambing
    private float moveToTrashSpeed = 10f; // Kecepatan bergerak ke kantong sampah

    private bool isMovingToTrash = false;

    private void OnMouseDown()
    {
        if (!isMovingToTrash)
        {
            MoveToTrashAndDestroy();
        }
    }

    public void StartFloatingAnimation(float speed, float distance)
    {
        floatDistance = distance; // Mengatur ulang jarak terombang-ambing
        floatSpeed = speed; // Mengatur ulang kecepatan terombang-ambing

        transform.DOMoveY(transform.position.y + floatDistance, floatSpeed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void MoveToTrashAndDestroy()
    {
        // Setel status menjadi sedang bergerak
        isMovingToTrash = true;

        // Stop animasi terombang-ambing
        transform.DOKill();

        // Tentukan target posisi kantong sampah (gantilah dengan posisi kantong sampah di game Anda)
        Vector3 targetPosition = new Vector3(7.7f, 3.7f, 0f);

        // Hitung durasi berdasarkan kecepatan relatif
        float moveToTrashDuration = Vector3.Distance(transform.position, targetPosition) / moveToTrashSpeed;

        // Mulai animasi bergerak ke kantong sampah
        transform.DOMove(targetPosition, moveToTrashDuration)
            .SetEase(Ease.Linear)
            .OnComplete(DestroyTrash); // Panggil metode DestroyTrash() setelah animasi selesai
    }

    private void DestroyTrash()
    {
        SeaCleanManager seaCleanManager = FindObjectOfType<SeaCleanManager>();
        seaCleanManager.TrashDestroyed();
        Destroy(gameObject);
    }
}
