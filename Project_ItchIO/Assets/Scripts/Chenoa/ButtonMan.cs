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
        pauseMenu.SetActive(true); //pauze menu is te zien
        Time.timeScale = 0; //freeze de game
    }
    public void Resume()
    {
        pauseMenu.SetActive(false); //het pauze menu is niet meer zichtbaar
        Time.timeScale = 1; //continue de game
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
        if (SceneManager.sceneCount == 1) //als je in de eerste scene in de buildIndex zit
        {
            SceneManager.LoadScene("Level1"); //reload de scene
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
