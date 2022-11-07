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
    #endregion

    [SerializeField] private TextMeshPro highScoreText;
    [SerializeField] private TextMeshPro scoreText;
    private int Score;
    private int HighScore = 0;

    private void Start()
    {
        SetScoreTexts();
    }

    private void SetScoreTexts()
    {
        scoreText.text = "Score: " + Score;
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
        if(Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
            highScoreText.text = "High score " + PlayerPrefs.GetInt("HighScore");
        }
    }
    private void AddScore()
    {
        Score = Random.Range(1, 10) * 10;
        SetScoreTexts();
    }

}
