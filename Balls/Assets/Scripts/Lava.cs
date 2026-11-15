using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

    private void Update(){
        transform.position = new Vector2(Player.Instance.transform.position.x, transform.position.y);
    }

    public void HitPlayer(){
        HealthManager.Instance.HealthChangeAdd(-HealthManager.Instance.maxHealth);
    }

}
