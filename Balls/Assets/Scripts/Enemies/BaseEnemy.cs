using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour {

    [SerializeField] protected ParticleSystem dieParticles;

    [SerializeField] protected SpriteRenderer spriteCircle;

    //stats
    protected int scoreValue;
    protected float healValue;
    protected float force;

    private void Start(){
    }

    public virtual void HitEnemy(){
    }


    private void Update()
    {
        if (transform.position.x < Player.Instance.transform.position.x - 100)
        {
            DestoryEnemy();

        }
    }

    protected void DestoryEnemy(){
        dieParticles.transform.SetParent(null, true);
        dieParticles.Play();
    
        DamagePopUpManager.Instance.spawnText(scoreValue*ScoreAndMultiplierManager.Instance.curMultiplier, transform.position+(Vector3.up*2f));

        SpawnEnemies.Instance.removeBall(gameObject);
        Destroy(gameObject);

    }
}
