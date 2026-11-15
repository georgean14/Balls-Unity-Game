using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArtificialBounceScript : MonoBehaviour
{
    [Header("Bounce Settings")]
    private float launchForce = 17f;
    private float launchAngle =15f; // Degrees from horizontal
    private float cycleDelay = 1.5f; // Time before restarting
    
    [Header("Spawn Position")]
    private Vector2 startPosition = new Vector2(-30f, 3f);
    
    private Rigidbody2D rb;

    private TrailRenderer trailRenderer;
    private bool isOffScreen = false;
    private float offScreenTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        // Set up the rigidbody for bouncy physics
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        // Start the first launch
        ResetAndLaunch();
    }

    void Update()
    {
        // Check if ball has exited to the right
        if (transform.position.x > Camera.main.orthographicSize * Camera.main.aspect + 2f)
        {
            if (!isOffScreen)
            {
                isOffScreen = true;
                offScreenTimer = 0f;
                trailRenderer.enabled=false;
            }
            
            offScreenTimer += Time.deltaTime;
            
            if (offScreenTimer >= cycleDelay)
            {
                ResetAndLaunch();
            }
        }
    }

    void ResetAndLaunch()
    {
        // Reset position
        transform.position = startPosition;

        trailRenderer.enabled=true;
        
        // Reset velocity
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        
        // Calculate launch direction
        float angleRad = launchAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        
        // Launch the ball
        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
        
        isOffScreen = false;
    }
}
