using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCaseCurrentBall : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameBalls;
    private int currentActive;
    private void Start()
    {
        // gameBalls[currentActive].SetActive(false);
        // gameBalls[CosmeticsManager.Instance.getSkinIdx()].SetActive(true);
        // if (CosmeticsManager.Instance == null) Debug.Log("byte");
        currentActive = CosmeticsManager.Instance.getSkinIdx();
        gameBalls[CosmeticsManager.Instance.getSkinIdx()].SetActive(true);
    }

    public void changeBall(int idx)
    {
        // Debug.Log("working here");
        gameBalls[currentActive].SetActive(false);
        currentActive = idx;
        // Debug.Log("cur game ball = "+currentActive);
        gameBalls[currentActive].SetActive(true);
    }

    private void OnEnable()
    {
        if (CosmeticsManager.Instance != null)
        {
            CosmeticsManager.Instance.OnSkinChanged += changeBall;
        }
    }

    private void OnDisable()
    {
        if (CosmeticsManager.Instance != null)
        {
            CosmeticsManager.Instance.OnSkinChanged -= changeBall;
        }
    }
}
