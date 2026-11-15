using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private float pullStrength=20f;
    private float pullRadius=10f;

    void FixedUpdate()
    {
        Rigidbody2D rb = Player.Instance.GetComponent<Rigidbody2D>(); 

        Vector2 dir = (Vector2)transform.position - rb.position;
        float dist = dir.magnitude;
        if (dist > pullRadius) return;

        dir/=dist; 

        float forcePercent = 1f - (dist / pullRadius);
        rb.AddForce(dir * pullStrength * forcePercent, ForceMode2D.Force);
    }
    
}
