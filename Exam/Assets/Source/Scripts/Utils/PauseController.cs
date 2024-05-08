using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas; 
    [SerializeField] KeyCode pauseKey; 

    bool paused = false; 

    private void Start()
    {
        Unpause(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey)) 
        {
            if (paused)
                Unpause(); 
            else
                Pause(); 
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f; 
        paused = true; 
        Cursor.lockState = CursorLockMode.None; 
        pauseCanvas.SetActive(true); 
    }

    public void Unpause()
    {
        Time.timeScale = 1f; 
        paused = false; 
        Cursor.lockState = CursorLockMode.Locked; 
        pauseCanvas.SetActive(false); 
    }
}
