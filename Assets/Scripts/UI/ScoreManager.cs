using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    public static ScoreManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private ScoreManager()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    [SerializeField] private TextMeshPro highScoreText;
    [SerializeField] private TextMeshPro scoreText;
    private int score;
    private int highScore = 0;

    private void Start()
    {
        SetScoreTexts();
    }

    private void SetScoreTexts()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High score " + PlayerPrefs.GetInt("HighScore");
        }
    }
    private void AddScore()
    {
        score = Random.Range(1, 10) * 10;
        SetScoreTexts();
    }

}
