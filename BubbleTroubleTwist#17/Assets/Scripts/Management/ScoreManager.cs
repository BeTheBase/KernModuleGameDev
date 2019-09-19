﻿using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Score management class that handles all score related behaviour
/// </summary>
public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// scoreText to store the score on screen. We use TextMeshProUGUI for nice looking text
    /// </summary>
    [SerializeField] private TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }

    /// <summary>
    /// scoreText string to get rid of hard coded strings
    /// </summary>
    [SerializeField] private string scoreTextString;
    public string ScoreTextString { get => scoreTextString; set => scoreTextString = value; }

    /// <summary>
    /// property score to store the score
    /// </summary>
    public int Score { get; set; }

    private void OnEnable()
    {
        //Subscribing the UpdateScore method so we can re-use it later on
        EventManager.OnScoreChangedHandler += UpdateScore;

    }

    private void Start()
    {
        //On the beginning of the game we set the score
        UpdateScoreText();
    }

    /// <summary>
    /// Setting the Score property and call the update text field method
    /// </summary>
    /// <param name="_amount">amount of score we want to add</param>
    public void UpdateScore(int _amount)
    {
        Score += _amount;
        UpdateScoreText();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Update the score text textfield 
    /// </summary>
    private void UpdateScoreText()
    {
        ScoreText.text = ScoreTextString + Score;
    }

    /// <summary>
    /// when this instance gets disabled we need to unsubscribe
    /// </summary>
    private void OnDisable()
    {
        EventManager.OnScoreChangedHandler -= UpdateScore;
    }
}