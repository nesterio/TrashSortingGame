using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    [HideInInspector]
    public static ScoreManager Instance;
    private void Awake()
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

    [SerializeField] private int minScore = 1;
    [SerializeField] private int maxScore = 10;
    [Space(5)]
    [SerializeField] private TextMeshPro highScoreText;
    [SerializeField] private TextMeshPro scoreText;
    public int Score { get; private set; }
    public int HighScore{ get; private set; } = 0;

    private void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        HighScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "HighScore: " + HighScore;
        SetScoreTexts();
    }

    private void SetScoreTexts()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");
        if(PlayerPrefs.GetInt("Score") > HighScore)
        {
            HighScore = PlayerPrefs.GetInt("Score");
            PlayerPrefs.SetInt("HighScore", HighScore);
            highScoreText.text = "High score " + HighScore;
        }
    }
    

    public void AddScore()
    {
        Score = (Random.Range(1, 10) * 10);
        PlayerPrefs.SetInt("Score", Score + PlayerPrefs.GetInt("Score"));
        SetScoreTexts();
    }

}
