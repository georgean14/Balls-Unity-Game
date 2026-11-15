using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBallDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameBalls;
    private int currentActive=-1;
    private void Start()
    {
        currentActive = CosmeticsManager.Instance.getSkinIdx();
        Debug.Log("here, currentActive= " + currentActive);
        gameBalls[currentActive].SetActive(true);
    }
}
