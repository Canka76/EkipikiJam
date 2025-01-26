using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public GameObject escapeMenu;

    private FpsController fpsController;
    private SkillManagerRefactor skillManager;
    
    bool isOpen = false;
    bool gamePause = false;
    
    void Awake()
    {
        fpsController = GetComponent<FpsController>();
        skillManager = GetComponent<SkillManagerRefactor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        if (gamePause)
        {
            ResumeGame(escapeMenu);
        }
        else
        {
            PauseGame(escapeMenu);
        }
    }

    void ResumeGame(GameObject menu)
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        gamePause = false;
        if (menu != null && isOpen)
        {
            isOpen = false;
            menu.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        if (fpsController != null)
        {
            fpsController.enabled = true;
        }
        if (skillManager != null)
        {
            skillManager.enabled = true;
        }
    }

    public void PauseGame(GameObject menu)
    {
        AudioListener.pause = true;
        Time.timeScale = 0f;
        gamePause = true;
        if (menu != null && !isOpen)
        {
            isOpen = true;
            menu.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if (fpsController != null)
        {
            fpsController.enabled = false;
        }
        if (skillManager != null)
        {
            skillManager.enabled = false;
        }
    }
    
    public void OnButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnButtonResume()
    {
        ResumeGame(escapeMenu);
    }
    
    public void OnButtonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame(escapeMenu);
    }
}
