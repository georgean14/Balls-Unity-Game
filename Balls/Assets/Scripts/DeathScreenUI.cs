using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeathScreenUI : MonoBehaviour {
    
    private const string PLAYER_PREFS_HIGH_SCORE = "HighScore";
    public float XPanimTime = 0.5f;
    public event EventHandler OnNewHighScore;

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI highScoreDisplay;
    [SerializeField] private TextMeshProUGUI distanceDisplay;
    [SerializeField] private TextMeshProUGUI enemiesHitDisplay;
    [SerializeField] private TextMeshProUGUI xpGainedDisplay;
    [SerializeField] private TextMeshProUGUI moneyHaveText;
    [SerializeField] private TextMeshProUGUI moneyEarnedText;
    [SerializeField] private GameObject newHighScore;
    [SerializeField] private Image xpBar;
    [SerializeField] private TextMeshProUGUI xpLevelDisplay;

    [SerializeField] private Button backToHomeButton;


    private float minDuration = 0.2f;
    private float maxDuration = 2f;
    private float secondsPerDollar = 0.1f; // tune this
    private Coroutine countRoutine;


    public bool xpAnimationPlayed;

    private void Awake()
    {
        backToHomeButton.onClick.AddListener(GoBackToHome);
    }

    private void Start()
    {
        
    }

    private void GoBackToHome()
    {

        SoundManager.Instance.PlayButtonClick();
        GameManager.Instance.loadHome();
    }

    private void UpdateScreen(){

        if(!GameManager.Instance.PlayedMoneyAnimation()){
            moneyHaveText.text="$"+(MoneyManager.Instance.getMoney()-GameManager.Instance.moneyEarnedThisLevel);
            moneyEarnedText.text = "+$"+GameManager.Instance.moneyEarnedThisLevel;
            AddMoney(MoneyManager.Instance.getMoney()-GameManager.Instance.moneyEarnedThisLevel,GameManager.Instance.moneyEarnedThisLevel);
            GameManager.Instance.PlayMoneyAnimation();
        }

        int score = ScoreAndMultiplierManager.Instance.GetScore();
        if(score > ScoreAndMultiplierManager.Instance.GetHighScore()){
            ScoreAndMultiplierManager.Instance.SetHighScore(score);
            newHighScore.SetActive(true);
            PlayerPrefs.SetInt(PLAYER_PREFS_HIGH_SCORE, score);
            OnNewHighScore?.Invoke(this, EventArgs.Empty);
        }else {
            newHighScore.SetActive(false);
        }
        Debug.Log("i'm here");
        highScoreDisplay.text = "HIGH SCORE: " + ScoreAndMultiplierManager.Instance.GetHighScore();

        enemiesHitDisplay.text="kills:  +"+ GameManager.Instance.getEnemiesHit();
        distanceDisplay.text="distance:  +"+ GameManager.Instance.getDistance();
        xpGainedDisplay.text="+ "+GameManager.Instance.GetXPGainedThisLevel()+" xp";

        int oldLevel=GameManager.Instance.GetCurLevel();
        int curLevel=GameManager.Instance.GetNextLevel();
        float oldFrac=GameManager.Instance.GetCurFrac();
        float curFrac=GameManager.Instance.GetNextFrac();

        // Debug.Log("old Level: "+ oldLevel);
        // Debug.Log("cur Level: "+ curLevel);
        // Debug.Log("old frac: "+ oldFrac);
        // Debug.Log("cur frac: "+ curFrac);
        // if(!xpAnimationPlayed) { 
            StartCoroutine(AnimateLevelUpSequence(oldFrac, curFrac, oldLevel, curLevel)); 
        //     xpAnimationPlayed=true;
        // }

    }
    
    IEnumerator AnimateLevelUpSequence(float oldFrac, float curFrac, int oldLevel, int curLevel)
    {
        if (curLevel == oldLevel)
        {
            xpLevelDisplay.text = "level: " + oldLevel;
            yield return StartCoroutine(XPAnim(oldFrac,curFrac));
        }else{
            xpLevelDisplay.text = "level: " + oldLevel;
            yield return StartCoroutine(XPAnim(oldFrac,1f));
            oldLevel++;

            while (oldLevel < curLevel)
            {
                xpLevelDisplay.text = "level: " + oldLevel;
                yield return StartCoroutine(XPAnim(0f,1f));
                oldLevel++;
            }

            xpLevelDisplay.text = "level: " + oldLevel;
            yield return StartCoroutine(XPAnim(0f,curFrac));
        }
    }

    public void AnimateXP(float oldValue, float newValue)
    {
        StartCoroutine(XPAnim(oldValue,newValue));
    }

    IEnumerator XPAnim(float start, float end)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / XPanimTime;
            xpBar.fillAmount = Mathf.Lerp(start, end, t);
            yield return null; // waits for next frame
        }

        xpBar.fillAmount = end; 
    }
    private void OnEnable(){
        UpdateScreen();
    }

    public void AddMoney(int initialAmount, int addAmount)
    {
        if (addAmount <= 0) return;

        if (countRoutine != null)
            StopCoroutine(countRoutine);

        int from = initialAmount;
        int to = initialAmount + addAmount;

        float duration = Mathf.Clamp(addAmount * secondsPerDollar, minDuration, maxDuration);

        countRoutine = StartCoroutine(CountUp(from, to, duration));
    }

    private IEnumerator CountUp(int from, int to, float duration)
    {
        yield return new WaitForSeconds(0.5f); 

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // Smooth ease-out
            float easedT = 1f - Mathf.Pow(1f - t, 3f);

            int value = Mathf.RoundToInt(Mathf.Lerp(from, to, easedT));
            moneyHaveText.text = "$" + value;

            yield return null;
        }

        moneyHaveText.text = "$" + to;
    }
}
