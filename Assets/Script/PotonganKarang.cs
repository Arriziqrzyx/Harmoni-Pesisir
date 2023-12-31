using UnityEngine;
using DG.Tweening;

public class PotonganKarang : MonoBehaviour
{
    private Transform targetKeranjang;

    public void SetTargetKeranjang(Transform keranjang)
    {
        targetKeranjang = keranjang;

        // Mulai animasi pergerakan menuju keranjang
        MoveTowardsKeranjang();
    }

    private void MoveTowardsKeranjang()
    {
        if (targetKeranjang != null)
        {
            float moveDuration = 2f; // Sesuaikan dengan durasi yang diinginkan

            // Gunakan DOTween untuk animasi pergerakan
            transform.DOMove(targetKeranjang.position, moveDuration).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // Hapus objek potongan karang setelah mencapai keranjang
                    Destroy(gameObject);
                });
        }
        else
        {
            Debug.LogError("Objek keranjang tidak ditentukan. Assign objek keranjang pada SpawnButton.");
        }
    }
}
