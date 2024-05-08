using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartScnee()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
