using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionEnemy : BaseEnemy {
    
    private Rigidbody2D playerRb;

    public override void HitEnemy() {

        playerRb=Player.Instance.GetComponent<Rigidbody2D>();
        scoreValue=40;
        healValue=30f;
        force=10f;
        HealthManager.Instance.HealthChangeAdd(healValue);
        ScoreAndMultiplierManager.Instance.ScoreChangeAdd(scoreValue);
        BounceRandomDir();
        
        DestoryEnemy();
    }
    
    private void BounceRandomDir(){

        float dirInRad = Random.Range(0f, 2f*Mathf.PI);
        playerRb.velocity += new Vector2(force * Mathf.Cos(dirInRad),force * Mathf.Sin(dirInRad));


    }
}
