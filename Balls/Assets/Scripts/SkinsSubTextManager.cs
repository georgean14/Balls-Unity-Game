using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinsSubTextManager : MonoBehaviour
{
    public static SkinsSubTextManager Instance{get;private set;}
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI subText;
    [SerializeField] private SkinGridManager skinGridManager;

    private int moneyNeededCur;
    private void Awake()
    {
        Instance=this;
        buyButton.onClick.AddListener(boughtSkin);
    }
    public void SetBuy(int moneyNeeded)
    {
        moneyNeededCur=moneyNeeded;
        buyButton.gameObject.SetActive(true);
        subText.text="$"+moneyNeeded;
        subText.fontSize=71.5f;
        subText.color = new Color(0f, 1f, 0.1f, 1f);
        subText.fontStyle=FontStyles.Bold;
    }
    public void SetText(string s)
    {
        buyButton.gameObject.SetActive(false);
        subText.text=s;
        subText.fontSize=35;
        subText.color = new Color(1f, 1f, 1f, 1f);
        subText.fontStyle=FontStyles.Normal;
    }
    private void boughtSkin()
    {
        if(MoneyManager.Instance.getMoney()>=moneyNeededCur){
            CosmeticsMenuSoundManager.Instance.PlayChainSFX();
            CosmeticsManager.Instance.UnlockSkin();
            skinGridManager.ClearGridLayout();
            skinGridManager.PopulateGrid();
            MoneyManager.Instance.loseMoney(moneyNeededCur);
        }
    }
}
