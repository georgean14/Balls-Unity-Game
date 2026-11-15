using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance{ get; private set; }

    public event EventHandler OnCollideWithEnemy;
    
    public bool isHolding=false;
    //slowmo stuff
    private float slowmoScale;
    private float slowmoVelocity;

    //squash stuff
    private Vector3 squashScale;
    private Vector3 defaultScale;
    private Vector3 velocitySquash;

    //jump stats
    private bool canJump;
    private float maxJumpForce=25f;

    private float jumpMultiplier=0.1f;

    private Rigidbody2D rb;

    [SerializeField] LineRenderer aimLine;
    [SerializeField] Transform playerVisualTransform;

    private bool isAiming;

    private float timeElapsedSinceLastHitEnemy=0f;
    private float multiplierTimeLimit=2f;
    private float combos=0f;

    private int maxCombo;

    private Vector2 mouseOriginPos;

    private void Awake(){
        Instance = this;
    }
    private void Start()
    {
        aimLine.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        squashScale = new Vector3(0.8f, 0.6f, 1f);
        defaultScale = transform.localScale;
        aimLine.positionCount = 2;
        canJump = true;
        isAiming = false;
        combos = 0;

        slowmoScale=UpgradeManager.Instance.getSlowMoVal();
        maxCombo=UpgradeManager.Instance.getComboVal();
        Debug.Log("max combo: "+maxCombo);
    }
    

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);

        Vector2 mousePosOnScreen = Input.mousePosition;
        // mousePos = new Vector2(playerPos.x-(mousePos.x-playerPos.x), playerPos.y-(mousePos.y-playerPos.y)); // fixes the direction
        HandleMovement(mousePos, playerPos);
        AdditionalMovementFeatures();
        HandleMultiplier();
    }

    private void HandleMovement(Vector2 mousePos, Vector2 playerPos){
        if((canJump || !canJump) && !AutoBounceUpgrade.Instance.isActive){
            if(Input.GetMouseButtonDown(0)){
                isAiming=true;
                mouseOriginPos = Input.mousePosition;
                isHolding=true;
            } 
            if(Input.GetMouseButton(0)){
                isHolding=true;
                Vector2 mouseCurPos = Input.mousePosition;
                Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);

                Vector2 targetScreenPos = playerScreenPos + (mouseOriginPos - mouseCurPos);
                Vector3 playerWorldPos = transform.position;
                Vector3 targetWorldPos = Camera.main.ScreenToWorldPoint(targetScreenPos); 
                targetWorldPos.z = playerWorldPos.z;
                Vector2 dirOfJump = (targetWorldPos - playerWorldPos).normalized;

                Vector2 dragVector=mouseOriginPos-mouseCurPos;
                float dragDistance = dragVector.magnitude;

                float jumpForce = Mathf.Clamp(dragDistance * jumpMultiplier, 0f, maxJumpForce);

                Vector2 velocity = dirOfJump * jumpForce;

                HandleAimLine(velocity, transform.position);
                aimLine.enabled=true;

                Time.timeScale = Mathf.SmoothDamp(Time.timeScale, slowmoScale, ref slowmoVelocity, 0.1f);

                playerVisualTransform.localScale = Vector3.SmoothDamp(playerVisualTransform.localScale, squashScale, ref velocitySquash, 0.2f);

                float angleOfRotation = Mathf.Atan2(dirOfJump.y, dirOfJump.x) * Mathf.Rad2Deg;
                playerVisualTransform.rotation = Quaternion.AngleAxis(angleOfRotation-90, Vector3.forward);

                //slowly remove health
                float damageTakenRate = 50f; // normally 
                HealthManager.Instance.HealthChangeAdd(-(damageTakenRate * Time.unscaledDeltaTime));
            }
            if(Input.GetMouseButtonUp(0)){
                isHolding=false;
                Time.timeScale=1;

                aimLine.enabled=false;


                Vector2 mouseCurPos = Input.mousePosition;
                Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);

                Vector2 targetScreenPos = playerScreenPos + (mouseOriginPos - mouseCurPos);
                Vector3 playerWorldPos = transform.position;
                Vector3 targetWorldPos = Camera.main.ScreenToWorldPoint(targetScreenPos); 
                targetWorldPos.z = playerWorldPos.z;
                Vector2 dirOfJump = (targetWorldPos - playerWorldPos).normalized;

                
                Vector2 dragVector=mouseOriginPos-mouseCurPos;
                float dragDistance = dragVector.magnitude;

                float jumpForce = Mathf.Clamp(dragDistance * jumpMultiplier, 0f, maxJumpForce);

                Vector2 velocity = dirOfJump * jumpForce;

                rb.velocity = velocity;

                playerVisualTransform.localScale = defaultScale;
            
                canJump=false;
                isAiming=false;
            }
        }else{//no jumps left

        }
    }
    private void HandleAimLine(Vector2 velocity, Vector3 playerPos){
        if(Camera.main == null){
            Debug.LogError("camera is null");
        }
        int segments=14; // number of points in the curve
        float timeStep =0.1f;// time in between points
        aimLine.positionCount = segments;
        for(int i = 0; i < segments; i++)
        {
            float time=i*timeStep;
            Vector3 point = playerPos + (Vector3)(velocity * time) + 0.5f * (Vector3)Physics2D.gravity * (time * time);
            aimLine.SetPosition(i, point);
        }
    }

    private void AdditionalMovementFeatures(){
        if(!isAiming){
            //squeeze while moving fast
            float playerSpeed= rb.velocity.magnitude; 
            float speedThreshold = 25f;
            float scaleMultiplier = Mathf.Clamp01(playerSpeed / speedThreshold);
            Vector3 targetScale = Vector3.Lerp(defaultScale, squashScale, scaleMultiplier);
            playerVisualTransform.localScale = Vector3.SmoothDamp(playerVisualTransform.localScale, targetScale, ref velocitySquash, 0.2f);
            //rotate towards velocity
            Vector2 playerVelocity = rb.velocity;
            if(rb.velocity!=Vector2.zero){
                float angle = Mathf.Atan2(playerVelocity.y, playerVelocity.x) * Mathf.Rad2Deg;
                playerVisualTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

        }

    }
    private void HandleMultiplier(){
        timeElapsedSinceLastHitEnemy+=Time.unscaledDeltaTime;
        if(timeElapsedSinceLastHitEnemy>multiplierTimeLimit){
            combos=0;
            ScoreAndMultiplierManager.Instance.curMultiplier =(int) Mathf.Clamp(combos, 1f, maxCombo);
            timeElapsedSinceLastHitEnemy=0;
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.TryGetComponent(out BaseEnemy enemy)){
            canJump=true;
            timeElapsedSinceLastHitEnemy=0;
            combos++;
            ScoreAndMultiplierManager.Instance.curMultiplier =(int) Mathf.Clamp(combos, 1f,maxCombo);
            OnCollideWithEnemy?.Invoke(this, EventArgs.Empty);
            enemy.HitEnemy();
        }else if(col.gameObject.TryGetComponent(out Lava lava)){
            lava.HitPlayer();
        }else{//hit something probably a floor
            canJump=true;
        }
    }
}
