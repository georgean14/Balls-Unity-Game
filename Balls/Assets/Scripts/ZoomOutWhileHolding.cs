using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

public class LensDistortionWhileHolding : MonoBehaviour {
    private CinemachineVirtualCamera virtualCamera;
    private float targetZoom = 12f; // Distortion amount when holding
    private float smoothSpeed = 10f; // How fast it transitions
    
    private float currentZoom = 10f;
    private void Awake(){
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start(){
        
    }

    private void Update(){
        float target = Player.Instance.isHolding ? targetZoom : 0f;
        currentZoom = Mathf.Lerp(currentZoom, target, Time.unscaledDeltaTime * smoothSpeed);
        virtualCamera.m_Lens.OrthographicSize=currentZoom;
    }

}
