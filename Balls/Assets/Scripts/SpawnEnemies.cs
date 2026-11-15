using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public static SpawnEnemies Instance{ get; private set; }

    public List<GameObject> allAliveBalls;

    [SerializeField] private GameObject redEnemy;
    [SerializeField] private GameObject blueEnemy;
    [SerializeField] private GameObject yellowEnemy;
    [SerializeField] private GameObject purpleEnemy;
    [SerializeField] private GameObject blackHole;
    [SerializeField] private Transform playerTransform;
    private float distance=0;
    private float distanceUsed=0;

    private float nextRender=50f;

    private void Awake(){
        Instance = this;
    }    
    private void Start(){
        SpawnEnemyAtStart(50);
    }

    private void Update(){
        if(distance < playerTransform.position.x + nextRender){
            distance = playerTransform.position.x + nextRender;
        }

        float distToGo = Mathf.Floor(distance - distanceUsed);

        if(distanceUsed < distance && distToGo>4){
            distanceUsed = distance;
            
            int enemyDensity = 8;
            SpawnEnemy(enemyDensity);
        }
    }

    private void SpawnEnemy(int enemyDensity) {
        for(int i=0;i<enemyDensity;i++){
            GameObject enemyToSpawn = SelectEnemyToSpawn();

            float yPos = Mathf.Floor(Mathf.Abs(Random.Range(0f, 1f) - Random.Range(0f, 1f)) * (1+100 - (-2))+ (-2));
            float xPos = distance + Random.Range(-10f,10f);
            Vector2 spawnPos = new Vector2(xPos, yPos);
            //Debug.Log("spawn");
            GameObject t = Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);

            if(enemyToSpawn!=blackHole) addBall(t);

        }
        
    }
    private void SpawnEnemyAtStart(int enemyDensity) {
        for(int i=0;i<enemyDensity;i++){
            GameObject enemyToSpawn = SelectEnemyToSpawn(true);

            float yPos = Mathf.Floor(Mathf.Abs(Random.Range(0f, 1f) - Random.Range(0f, 1f)) * (1+100 - (-2))+ (-2));
            float xPos = distance + Random.Range(10f,30f);
            Vector2 spawnPos = new Vector2(xPos, yPos);
            //Debug.Log("spawn");
            GameObject t = Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);

            if(enemyToSpawn!=blackHole) addBall(t);
        }
        
    }

    private GameObject SelectEnemyToSpawn(bool begin=false){
        //spawn red with 50% chance
        // blue has 20%
        //spawn purple with 20%
        //swawn yellow and hole with 5%

        //ONLY SPAWNING RED TO CHANGE BACK CHANGE LINE 69 TO SPAWNINT<=5

        int spawnInt = Random.Range(1,21);
        if(spawnInt<=10) return redEnemy;
        else if (spawnInt<=14) return blueEnemy; 
        else if(spawnInt<=18) return purpleEnemy;
        else if(spawnInt<=19) return yellowEnemy;
        else if(!begin) return blackHole;
        else return redEnemy;
        // if(spawnInt<=10) return redEnemy;
        // else return blueEnemy;
    }

    public void addBall(GameObject ball) {allAliveBalls.Add(ball);}
    public void removeBall(GameObject ball) {allAliveBalls.Remove(ball);}
}
