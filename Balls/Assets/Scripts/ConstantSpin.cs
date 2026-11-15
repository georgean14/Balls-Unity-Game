using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpin : MonoBehaviour {

    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    private void Update() {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(0,0,rotationAmount);
    }
}
