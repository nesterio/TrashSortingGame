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
    public int score { get; private set; }
    public int highScore{ get; private set; } = 0;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "HighScore: " + highScore;
        SetScoreTexts();
    }

    private void SetScoreTexts()
    {
        scoreText.text = "Score: " + score;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High score " + highScore;
        }
    }
    

    public void AddScore()
    {
        score = Random.Range(1, 10) * 10;
        SetScoreTexts();
    }

}
