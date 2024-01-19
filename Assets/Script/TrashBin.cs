using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public Sprite openedBinSprite;
    public Sprite closedBinSprite;
    public AudioSource audioMangap;

    private SpriteRenderer spriteRenderer;
    private float originalYPosition;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalYPosition = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Objek sampah bersentuhan dengan tong sampah
        if (other.CompareTag("Trash"))
        {
            spriteRenderer.sprite = openedBinSprite;
            ChangeBinSpritePosition(-1.13f);
            audioMangap.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Objek sampah tidak bersentuhan dengan tong sampah lagi
        if (other.CompareTag("Trash"))
        {
            spriteRenderer.sprite = closedBinSprite;
            ChangeBinSpritePosition(originalYPosition);
        }
    }

    private void ChangeBinSpritePosition(float newYPosition)
    {
        Vector3 newPosition = transform.position;
        newPosition.y = newYPosition;
        transform.position = newPosition;
    }
}
