using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class AutoBounceUpgrade : MonoBehaviour{

    public static AutoBounceUpgrade Instance{ get; private set; }

    [SerializeField] private Player player;

    [SerializeField] private Button button;
    [SerializeField] private Image shadedOverlay;
    private float TimeLimit=3f;
    private float timerAbility=0f;

    private float flyingSpeed=40f;
    public bool isActive=false;

    int enemiesNeededToActivate=15;
    int currentEnemiesCount=0;

    private GameObject currentTarget;

    private void Awake()
    {
        Instance=this;
        button.onClick.AddListener(OnActivateAbilityClicked);
        player.OnCollideWithEnemy+=CollidedWithEnemy;
    }
    private void Start()
    {
        currentEnemiesCount=0;
        isActive=false;
        currentTarget=null;
        button.gameObject.SetActive(true);
    }
    private void Update()
    {
        if(!isActive)UpdateUI(currentEnemiesCount,enemiesNeededToActivate);
        // Debug.Log(isActive);
        if(isActive){
            timerAbility-=Time.unscaledDeltaTime;
            UpdateUI(timerAbility, TimeLimit);
            if(timerAbility <= 0f) {
                // Debug.Log("TIMER EXPIRED - finishing last target");
            }
            // Debug.Log("time left : "+ timerAbility);
        }
        if (isActive && currentTarget == null){

            if(timerAbility>0f) currentTarget = GetNearestBall();
            else
            {
                isActive=false;
                currentEnemiesCount=0;
            }
            // Debug.Log($"GetNearestBall returned: {currentTarget?.name ?? "NULL"}");
        }

        if (currentTarget != null)
        {
            // Vector2 dir = (currentTarget.transform.position - player.transform.position).normalized;
            
            // playerRb.velocity=dir * flyingSpeed;

            player.transform.position = Vector2.MoveTowards(
                player.transform.position,
                currentTarget.transform.position,
                flyingSpeed * Time.deltaTime
            );

        }
    }

    private void CollidedWithEnemy(object sender, EventArgs e)
    {
        if(isActive) return;
        currentEnemiesCount++;
        //update ui
        UpdateUI(currentEnemiesCount,enemiesNeededToActivate);
    }
    private void UpdateUI(float numerator, float denominator)
    {
        float frac = 1-numerator/denominator;
        frac = Mathf.Clamp01(frac);
        shadedOverlay.fillAmount = Mathf.Lerp(shadedOverlay.fillAmount, frac, Time.unscaledDeltaTime*10f);
    }
    private void OnActivateAbilityClicked()
    {
        if(currentEnemiesCount>=enemiesNeededToActivate){
            isActive=true;
            Time.timeScale =1;
            timerAbility=TimeLimit;
            currentEnemiesCount=0;
            UpdateUI(currentEnemiesCount,enemiesNeededToActivate);
        }
    }

    private GameObject GetNearestBall()
    {
        float minDist=Mathf.Infinity;
        GameObject nearest=null;
        foreach(GameObject ball in SpawnEnemies.Instance.allAliveBalls)
        {
            if (ball == null)
            {
                Debug.Log("ball is null, trying to find a dead ball");
                continue;
            }
            float dist = ((Vector2)player.transform.position-(Vector2)ball.transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = ball;
            }
        }
        return nearest;
    }
}
