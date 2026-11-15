using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject shopGameObject;
    [SerializeField] private GameObject deathScreenObject;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private Button enterShopButton;
    [SerializeField] private Button exitShopButton;
    [SerializeField] private Image blackOverlayImage;


    private void Start()
    {

        exitShopButton.onClick.AddListener(OnShopLeft);
        enterShopButton.onClick.AddListener(OnShopClicked);

        shopGameObject.SetActive(false);
        deathScreenObject.SetActive(false);
        scoreText.SetActive(true);
        inGameUI.SetActive(true);
        blackOverlayImage.enabled=false;

        GameManager.Instance.OnDied += GameManager_OnDied;
    }

    private void GameManager_OnDied(object sender, EventArgs e)
    {
        OnDeath();
    }

    private void OnDeath()
    {
        shopGameObject.SetActive(false);
        deathScreenObject.SetActive(true);
        scoreText.SetActive(true);
        inGameUI.SetActive(false);
        blackOverlayImage.enabled=true;
    }

    private void OnShopClicked()
    {

        SoundManager.Instance.PlayButtonClick();
        shopGameObject.SetActive(true);
        deathScreenObject.SetActive(false);
        scoreText.SetActive(false);
        inGameUI.SetActive(false);
    }
    private void OnShopLeft()
    {

        SoundManager.Instance.PlayButtonClick();
        shopGameObject.SetActive(false);
        deathScreenObject.SetActive(true);
        scoreText.SetActive(true);
        inGameUI.SetActive(false);
    }

}
