using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreAndMultiplierManager : MonoBehaviour {

    public static ScoreAndMultiplierManager Instance;

    private const string PLAYER_PREFS_HIGH_SCORE = "HighScore";

    // public event EventHandler OnScoreChange;

    public event Action<int> OnScoreChange;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;

    private int score = 0;
    private int pastScore=-1;
    private int highScore=0;

    public int curMultiplier=1;

    private void Awake(){
        Instance = this;

        // PlayerPrefs.SetInt(PLAYER_PREFS_HIGH_SCORE, 0); //DELETE THIS LINE TO MAKE IT WORK
        highScore= PlayerPrefs.GetInt(PLAYER_PREFS_HIGH_SCORE,0);
    }

    private void Start(){
        curMultiplier=1;
        score=0;
    }

    private void Update(){
        //score text update
        if(score!=pastScore){
            // Debug.Log("basic get current score to be " +score);

            OnScoreChange?.Invoke(score);
            pastScore=score;
        }
        scoreText.text = "Score: " + score;

        //multiplier text
        if(curMultiplier==1){
            multiplierText.text=" ";
        }else{
            multiplierText.text = "x"+curMultiplier;
        }
    }

    public void ScoreChangeAdd(int addScore){
        score += addScore * curMultiplier;
    }
    
    public int GetScore(){
        return score;
    }

    public int GetHighScore(){
        Debug.Log("high score is "+ highScore);
        return highScore;
    }

    public void SetHighScore(int score){
        highScore=score;
    }
}

public class ScoreChangedEventArgs : EventArgs
{
    public int currentScore { get; private set; }

    public ScoreChangedEventArgs(int score)
    {
        currentScore = score;
    }
}