using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    
    public void Setup (int Score)
    {
        gameObject.SetActive (true);
        pointsText.text = Score.ToString() + " POINTS";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
