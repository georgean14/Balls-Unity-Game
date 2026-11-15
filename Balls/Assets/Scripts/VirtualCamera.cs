using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VirtualCamera : MonoBehaviour {
    private CinemachineVirtualCamera virtualCamera;

    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeIntensity = 20f;
    private float shakeDuration = 0.1f;

    private Rigidbody2D playerRb;

    private void Awake(){
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start(){
        playerRb = Player.Instance.GetComponent<Rigidbody2D>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        Player.Instance.OnCollideWithEnemy += Player_OnCollideWithEnemy;
    }

    private void LateUpdate(){

        // float maxCameraSize =15f;
        // float minCameraSize = 10f;
        // float targetCameraSizeBasedOnHeight = (Player.Instance.transform.position.y+4) /2;
        // float targetCameraSizeBasedOnSpeed = playerRb.velocity.magnitude/3;
        // float targetCameraSize = (targetCameraSizeBasedOnHeight+targetCameraSizeBasedOnSpeed)/2;
        // float cameraSize = Mathf.Clamp(Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetCameraSize, 0.2f),minCameraSize , maxCameraSize);
        float ratio = 4f/5f;
        float maxCameraSize = 15f * (ratio);
        float minCameraSize = 10f * (ratio);
        float targetCameraSizeBasedOnHeight = (Player.Instance.transform.position.y + 4) / 2 * (ratio);
        float targetCameraSizeBasedOnSpeed = playerRb.velocity.magnitude / 3 * (ratio);
        float targetCameraSize = (targetCameraSizeBasedOnHeight + targetCameraSizeBasedOnSpeed) / 2; // This average remains the same, but its components are scaled.
        if(Player.Instance.isHolding) targetCameraSize+=5;
        float cameraSize = Mathf.Clamp(Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetCameraSize, 0.01f), minCameraSize, maxCameraSize);
        virtualCamera.m_Lens.OrthographicSize=cameraSize;
    }

    private void Player_OnCollideWithEnemy(object sender, System.EventArgs e){
        ShakeTheCamera();
    }

    public void ShakeTheCamera()
    {
        StartCoroutine(shakeCamera());
    }

    private IEnumerator shakeCamera(){
        noise.m_AmplitudeGain = shakeIntensity;
        yield return new WaitForSeconds(shakeDuration);
        noise.m_AmplitudeGain=0f;
    }
}
