using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public static bool isActive = true;
    public GameObject MainMenu;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void EnterGame()
    {
        isActive = false;
        Time.timeScale = 1;

        MainMenu.SetActive(false);
    }
    public void CloseGame()
    {
        isActive = false;
        if (Application.isEditor)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
        else
        {
            Application.Quit();
        }
    }
}
