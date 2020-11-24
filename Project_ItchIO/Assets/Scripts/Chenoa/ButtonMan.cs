using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMan : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject titleScreen;

    private void Update()
    {
        if (SceneManager.sceneCount == 1)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void RestartLevel()
    {
        if (SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        //else if scenemanager.scenecount == 2
        //loadscene level2
    }

    public void GoToSettingsMenu()
    {
        settingsMenu.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
