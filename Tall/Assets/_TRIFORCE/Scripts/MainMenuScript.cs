using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenu;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void EnterGame()
    {
        Time.timeScale = 1;

        MainMenu.SetActive(false);
    }
    public void CloseGame()
    {
        if (Application.isEditor)
        {
            EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
