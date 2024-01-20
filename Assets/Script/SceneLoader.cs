using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string sceneToLoad; // Nama scene yang akan dimuat

    public void LoadScene(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene(sceneToLoad);
    }
    
    public void Keluar()
    {
        Application.Quit();
        Debug.Log("Keluar Game");
    }
}
