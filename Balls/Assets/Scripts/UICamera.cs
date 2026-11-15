using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class UICamera : MonoBehaviour
{
    // private int originalPriority;

    public static UICamera Instance{get; private set;}
    [SerializeField] private RectTransform canvasTransform;
    private float intensity = 15f;
    private float duration = 0.3f;

    // Vector2 originalPos;
    Coroutine shakeRoutine;
    
    
    private void Awake(){
        Instance=this;
        // originalPos = canvasTransform.anchoredPosition;
    }

    public void ShakeTheCamera()
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        float elapsed = 0f;

        Vector2 originalMin = canvasTransform.offsetMin;
        Vector2 originalMax = canvasTransform.offsetMax;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Vector2 offset = Random.insideUnitCircle * intensity;

            canvasTransform.offsetMin = originalMin + offset;
            canvasTransform.offsetMax = originalMax + offset;

            yield return null;
        }

        canvasTransform.offsetMin = originalMin;
        canvasTransform.offsetMax = originalMax;
    }
}
