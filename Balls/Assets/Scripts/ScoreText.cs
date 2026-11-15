using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreText : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;

    private Vector3 originalScale;

    private void Start(){
        ScoreAndMultiplierManager.Instance.OnScoreChange += GameManager_OnScoreChange;
        originalScale = scoreText.transform.localScale;
    }
    private void GameManager_OnScoreChange(int currentScore){
        // Debug.Log("bouncy bouncy " + currentScore);

        StartCoroutine(BounceText());
    }

    private IEnumerator BounceText() { 
        float duration = 0.1f;
        float elapsedTime = 0f;
        float scale=2f;
        Vector3 targetScale = originalScale * scale;
        while (elapsedTime < duration) {
            scoreText.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        scoreText.transform.localScale = originalScale; 
    }
}
