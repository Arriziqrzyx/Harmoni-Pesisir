using UnityEngine;

public class SeaweedController : MonoBehaviour
{
    private void OnMouseDown()
    {
        DestroySeaweed();
    }

    private void DestroySeaweed()
    {
        SeaCleanManager seaCleanManager = FindObjectOfType<SeaCleanManager>();
        seaCleanManager.SeaweedDestroyed();
        Destroy(gameObject);
    }
}
