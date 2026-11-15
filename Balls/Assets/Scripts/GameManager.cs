using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


    private const string PLAYER_PREFS_LEVEL = "Level";
    private const string PLAYER_PREFS_XP_AMT = "XPAmt";
    public static GameManager Instance;

    public event EventHandler OnDied;

    [SerializeField] ParticleSystem playerDieParticle;
    [SerializeField] GameObject player;
    [SerializeField] Player playerScript;

    private bool dead=false;

    //Stats
    private int curLevel, nextLevel;
    private int currentXP, nextXP;
    private float distance;
    private float curSpeed;
    private int totalEnemiesHit;
    private int XPGainedThisLevel;

    public int moneyEarnedThisLevel;

    private bool moneyAnimation;

    private void Awake(){
        Instance=this;
        // PlayerPrefs.DeleteAll();
    }

    private void Start()
    {

        playerScript.OnCollideWithEnemy+=CollidedWithEnemy;
        totalEnemiesHit=0;
        distance=0;

        curLevel=PlayerPrefs.GetInt(PLAYER_PREFS_LEVEL);
        currentXP=PlayerPrefs.GetInt(PLAYER_PREFS_XP_AMT);


    }

    private void Update()
    {
        distance = Math.Max(distance, Player.Instance.transform.position.x);
    }

    private void CollidedWithEnemy(object sender, EventArgs e)
    {
        totalEnemiesHit++;
    }

    public void Dead(){
        Time.timeScale=1;

        dead=true;

        playerDieParticle.transform.parent=null;
        player.SetActive(false);
        playerDieParticle.Play();
        
        // calculate next level and next xp

        // float weightOfDist=0.1f;
        float weightOfEnemiesHit=2f;
        float distExp=0.5f;
        float enemiesHitExp=1.2f;

        int gainedByEnemiesHit= (int)(weightOfEnemiesHit*((int)Math.Pow((ScoreAndMultiplierManager.Instance.GetScore()/11), enemiesHitExp)));
        int gainedByDist= (int)(3*Math.Pow(distance, distExp));

        XPGainedThisLevel=Mathf.Max(0,gainedByDist+gainedByEnemiesHit);

        moneyEarnedThisLevel=XPGainedThisLevel*2;
        moneyEarnedThisLevel=(int)(moneyEarnedThisLevel*UpgradeManager.Instance.getIncreasedIncomeVal());
        MoneyManager.Instance.earnMoney(moneyEarnedThisLevel);
        
        nextLevel=curLevel;
        nextXP=currentXP;

        nextXP+=XPGainedThisLevel;
        if (nextXP >= getXPNeededForNextLevel(nextLevel))
        {
            XPGainedThisLevel-=getXPNeededForNextLevel(nextLevel)-currentXP;
            nextLevel++;
            while (XPGainedThisLevel >= getXPNeededForNextLevel(nextLevel))
            {
                XPGainedThisLevel -= getXPNeededForNextLevel(nextLevel);
                nextLevel++;
            }
            nextXP=XPGainedThisLevel;
        }
        moneyAnimation=false;
        OnDied?.Invoke(this, EventArgs.Empty);
        // Debug.Log("HEREHEHEHREHREH");
        currentXP=nextXP;
        curLevel=nextLevel;
        PlayerPrefs.SetInt(PLAYER_PREFS_LEVEL, curLevel);
        PlayerPrefs.SetInt(PLAYER_PREFS_XP_AMT, currentXP);
    }
    public bool IsDead(){
        return dead;
    }
    public void reloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void loadHome(){
        SceneManager.LoadScene(0);
    }

    public int GetCurLevel(){
        return curLevel;
    }
    public float GetCurFrac(){
        // Debug.Log("cur Level: "+ curLevel);
        // Debug.Log("cur xp: "+ currentXP);
        return ((float)currentXP)/getXPNeededForNextLevel(curLevel);
    }

    public int GetNextLevel(){
        return nextLevel;
    }
    public float GetNextFrac(){
        // Debug.Log("get nxt frac next Level: "+ nextLevel);
        // Debug.Log("get nxt frac next xp: "+ nextXP);
        return ((float)nextXP)/getXPNeededForNextLevel(nextLevel);
    }

    private int getXPNeededForNextLevel(int lvl)
    {
        int a= 120;
        int c=100;
        return a*((int)Math.Pow(lvl, 1.5))+c;
    }
    public int getDistance()
    {
        return (int)distance;
    }
    public int getEnemiesHit()
    {
        return totalEnemiesHit;
    }
    public int GetXPGainedThisLevel()
    {
        return XPGainedThisLevel;
    }

    public bool PlayedMoneyAnimation()
    {
        return moneyAnimation;
    }
    public void PlayMoneyAnimation()
    {
        moneyAnimation=true;
    }
}
