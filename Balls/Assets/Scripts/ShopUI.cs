using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopUI : MonoBehaviour
{
    // [SerializeField] private Button enterShopButton;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private RectTransform screensParent;

    private int screenIdx=0;
    private float screenMoveTime=0.5f;
    private float screenWidth=2000f;
    private Coroutine moveRoutine;

    private void Awake(){
        // enterShopButton.onClick.AddListener(OnEnterShopClicked);
        leftButton.onClick.AddListener(OnLeftClicked);
        rightButton.onClick.AddListener(OnRightClicked);
    }

    private void Start()
    {
        screenIdx=1;
        moneyText.text="$"+ MoneyManager.Instance.getMoney();
        MoneyManager.Instance.OnMoneyChanged+=moneyChanged;
    }

    private void moneyChanged(object sender,EventArgs e)
    {
        moneyText.text="$"+ MoneyManager.Instance.getMoney();
    }

    private void OnLeftClicked()
    {
        SoundManager.Instance.PlayButtonClick();
        SetScreenIdx(screenIdx-1);
    }
    private void OnRightClicked()
    {
        SoundManager.Instance.PlayButtonClick();
        SetScreenIdx(screenIdx+1);
    }
    private void SetScreenIdx(int newIdx)
    {
        int numberOfScreens=2;
        newIdx=Mathf.Clamp(newIdx, 0,numberOfScreens);
        if (newIdx == screenIdx) return;
        screenIdx=newIdx;
        if (screenIdx == 0)
        {
            leftButton.enabled=false;
        }
        else if(screenIdx == numberOfScreens)
        {
            rightButton.enabled=false;
        }
        else
        {
            rightButton.enabled=true;
            leftButton.enabled=true;
        }
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(LerpTo((1-screenIdx) * screenWidth));
    }

    IEnumerator LerpTo(float targetX)
    {
        float elapsed = 0f;
        Vector2 start = screensParent.anchoredPosition;
        Vector2 end = new Vector2(targetX, start.y);

        while (elapsed < screenMoveTime)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / screenMoveTime;
            t = Mathf.Clamp01(t);

            // smoothstep easing (accel → decel)
            t = t * t * (3f - 2f * t);
            screensParent.anchoredPosition = Vector2.Lerp(start, end, t);
            yield return null;
        }

        screensParent.anchoredPosition = end;
    }
}
