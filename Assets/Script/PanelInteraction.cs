using UnityEngine;

public class PanelInteraction : MonoBehaviour
{
    public GameObject[] uiPanels;
    public Collider2D[] spriteColliders;

    void Update()
    {
        bool isAnyPanelActive = false;

        // Cek jika setidaknya satu panel aktif
        for (int i = 0; i < uiPanels.Length; i++)
        {
            if (uiPanels[i].activeSelf)
            {
                isAnyPanelActive = true;
                break;
            }
        }

        // Nonaktifkan semua collider jika setidaknya satu panel aktif
        for (int i = 0; i < spriteColliders.Length; i++)
        {
            Collider2D spriteCollider = spriteColliders[i];

            if (spriteCollider != null)
            {
                spriteCollider.enabled = !isAnyPanelActive;
            }
        }

        // Mengaktifkan kembali semua collider jika tidak ada panel yang aktif
        if (!isAnyPanelActive)
        {
            for (int i = 0; i < spriteColliders.Length; i++)
            {
                Collider2D spriteCollider = spriteColliders[i];

                if (spriteCollider != null)
                {
                    spriteCollider.enabled = true;
                }
            }
        }
    }
}
