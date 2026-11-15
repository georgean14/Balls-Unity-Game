using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawns : MonoBehaviour {

    [SerializeField] private GameObject groundPrefab;

    private float distBetweenGround=50f;
    
    List<GameObject> grounds = new List<GameObject>(); // ordered by ground x-pos

    float yPosOfGround = -6.5f;

    private float curMin = -0f, curMax=0f;

    private void Update(){
        float playerXPos = Player.Instance.transform.position.x;

        //Debug.Log("curmin: "+ curMin+", curmax: "+curMax+", playerxpos = " + playerXPos);

        //check right
        if (playerXPos + distBetweenGround > curMax) {
            SpawnGround(curMax + distBetweenGround);
            curMax += distBetweenGround;
        }
        //check left
        else if(playerXPos - distBetweenGround < curMin){
            SpawnGround(curMin - distBetweenGround); 
            curMin -= distBetweenGround;
        }

        DeleteGround(playerXPos);
    }

    private void SpawnGround(float xPos){
        Vector2 spawnPos = new Vector3(xPos, yPosOfGround,10);
        GameObject newGround = Instantiate(groundPrefab, spawnPos, Quaternion.identity);
        grounds.Add(newGround);
    }

    private void DeleteGround(float playerXPos){
        for(int i=grounds.Count-1; i>=0;i--){
            float groundXPos = grounds[i].transform.position.x;
            if(groundXPos+distBetweenGround < playerXPos){
                Destroy(grounds[i]);
                grounds.RemoveAt(i);
                curMin+=distBetweenGround;
            }
            if(groundXPos-distBetweenGround > playerXPos){
                Destroy(grounds[i]);
                grounds.RemoveAt(i);
                curMax-=distBetweenGround;
            }
        }
    }
}
