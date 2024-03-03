using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject PauseMenu;

    public static bool isPaused;

    private void Awake()
    {
        PauseMenu.SetActive(false);
        isPaused = false;
    }
    private void Update()
    {
        if (MainMenuScript.isActive) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if(PauseMenu.activeInHierarchy) StartCoroutine(ResumeGame());
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ExecuteResumeGame() => StartCoroutine(ResumeGame());

    public void BackToMainMenu()
    {
        Application.Quit();
    }    
    public IEnumerator ResumeGame()
    {   
        PauseMenu.SetActive(false);
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1;
        isPaused = false;
    }
}
