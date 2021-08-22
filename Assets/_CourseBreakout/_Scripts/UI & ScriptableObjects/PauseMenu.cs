using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public  bool isPaused;
    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
                settingsMenu.SetActive(false);
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    { 
        pauseMenu.SetActive(true);
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ToMenuButton()
    {
        SceneManager.LoadScene("MainMenu Scene");
    }
    public void ToSettings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void BackButton()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
