using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    
    public static HealthManager Instance;

    [SerializeField] private Image healthBarGreen;
    [SerializeField] private Image healthBarRed;

    private float health;
    public float maxHealth;

    private void Awake(){
        Instance=this;
    }

    private void Start(){
        maxHealth=UpgradeManager.Instance.getZingVal();
        health=maxHealth;

        // Debug.Log("maxhealth: "+maxHealth+", health="+health);

        GameManager.Instance.OnDied += GameManager_OnDied;
    }

    private void Update(){

        healthBarGreen.fillAmount = health/maxHealth;

        if(health<=0 && !GameManager.Instance.IsDead()) {
            GameManager.Instance.Dead();
        }
    }

    private void GameManager_OnDied(object sender, EventArgs e){
        healthBarGreen.enabled=false;
        healthBarRed.enabled=false;
    }

    public void HealthChangeAdd(float damage){
        health+=damage;
        health = Mathf.Clamp(health, 0f, maxHealth);
    }

}
