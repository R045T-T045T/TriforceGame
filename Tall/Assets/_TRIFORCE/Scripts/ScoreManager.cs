using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI highscoreText;

    int score = 0;
    int highscore = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString() + " POINTS";
        if(score > highscore) highscore = score;
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    public static void Increase() => instance.AddPoint();

    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString() + " POINTS";
        if(score > highscore) highscore = score;
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    private void ResetS()
    {
        score = 0;
        scoreText.text = score.ToString() + " POINTS";
        if (score > highscore) highscore = score;
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    public static void ResetScore()
    {
        instance.ResetS();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
