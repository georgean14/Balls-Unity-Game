using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{

    private const string PLAYER_PREFS_MONEY = "Money";

    public static MoneyManager Instance{ get; private set; }

    // [SerializeField] private TextMeshProUGUI moneyText;

    
    public event EventHandler OnMoneyChanged;

    private int curMoney;
    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance=this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        curMoney=PlayerPrefs.GetInt(PLAYER_PREFS_MONEY,0);
    }

    public int getMoney()
    {
        Debug.Log("current money: "+curMoney);
        return curMoney;
    }
    public void loseMoney(int x)
    {
        curMoney-=x;
        PlayerPrefs.SetInt(PLAYER_PREFS_MONEY, curMoney);
        OnMoneyChanged?.Invoke(this, EventArgs.Empty);
    }
    public void earnMoney(int x)
    {
        // Debug.Log("raw money: "+ x+", multipler: "+UpgradeManager.Instance.getIncreasedIncomeVal());
        // int addedMoney=(int)(x*UpgradeManager.Instance.getIncreasedIncomeVal());
        // Debug.Log("addedmoney = "+addedMoney);
        curMoney+=x;
        PlayerPrefs.SetInt(PLAYER_PREFS_MONEY, curMoney);
        OnMoneyChanged?.Invoke(this, EventArgs.Empty);
    }
}
