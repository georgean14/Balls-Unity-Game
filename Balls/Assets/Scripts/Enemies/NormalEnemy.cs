using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : BaseEnemy {

    private Rigidbody2D playerRb;

    public override void HitEnemy(){
        playerRb=Player.Instance.GetComponent<Rigidbody2D>();
        scoreValue=10;
        healValue=30f;
        force=10f;
        HealthManager.Instance.HealthChangeAdd(healValue);
        ScoreAndMultiplierManager.Instance.ScoreChangeAdd(scoreValue);
        BounceUpwards();

        DestoryEnemy();
    }


    private void BounceUpwards()
    {
        playerRb.velocity = new Vector2(0, force);
    }
}
