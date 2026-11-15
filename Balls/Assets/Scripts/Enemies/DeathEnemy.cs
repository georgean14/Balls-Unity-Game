using System;
using UnityEngine;

public class DeathEnemy : MonoBehaviour {
    [SerializeField] protected ParticleSystem dieParticles;

    public void OnCollisionEnter2D(Collision2D col){
        if (AutoBounceUpgrade.Instance.isActive)
        {
            dieParticles.transform.parent=null;
            dieParticles.Play();
            SpawnEnemies.Instance.removeBall(gameObject);
            Destroy(gameObject);
            return;
        }
        dieParticles.transform.parent=null;
        dieParticles.Play();
        int scoreValue=0;
        float healValue=-HealthManager.Instance.maxHealth;

        HealthManager.Instance.HealthChangeAdd(healValue);
        ScoreAndMultiplierManager.Instance.ScoreChangeAdd(scoreValue);
    }
}
