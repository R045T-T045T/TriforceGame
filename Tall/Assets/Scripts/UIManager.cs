using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    public void StartGame()
    {
        mainMenu.SetActive(false);
        LevelGeneration.SetMoveStatus(true);
        MainMenuScript.isActive = false;
    }

    public void ExitGame() => Application.Quit();
}
