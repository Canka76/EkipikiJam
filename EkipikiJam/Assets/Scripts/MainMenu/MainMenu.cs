using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public string newGameScene = "Game";
    [Header("Pages")]
    public GameObject settingsPage;
    public GameObject creditsPage;
    
    private bool settingsOpen = false;
    private bool creditsOpen = false;
    
    public void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene(newGameScene);
    }
    
    public void OnSettingsPageButtonClick()
    {
        var width = settingsPage.GetComponent<RectTransform>().rect.width;
        if (!creditsOpen)
        {
            if (settingsOpen)
            {
                settingsPage.transform.position += new Vector3(width, 0, 0);
                settingsOpen = false;
            }
            else
            {
                settingsPage.transform.position -= new Vector3(width, 0, 0);
                settingsOpen = true;
            }
        } else {
            settingsPage.transform.position -= new Vector3(width, 0, 0);
            creditsPage.transform.position += new Vector3(width, 0, 0);
            settingsOpen = true;
            creditsOpen = false;
        }
    }
    
    public void OnCreditPageButtonClick()
    {
        var width = creditsPage.GetComponent<RectTransform>().rect.width;
        if (!settingsOpen)
        {
            if (creditsOpen)
            {
                creditsPage.transform.position += new Vector3(width, 0, 0);
                creditsOpen = false;
            }
            else
            {
                creditsPage.transform.position -= new Vector3(width, 0, 0);
                creditsOpen = true;
            }
        } else {
            creditsPage.transform.position -= new Vector3(width, 0, 0);
            settingsPage.transform.position += new Vector3(width, 0, 0);
            settingsOpen = false;
            creditsOpen = true;
        }
    }
    
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
