using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{

    public static UpgradeManager Instance{ get; private set; }
    [SerializeField] private MoreZingUpgrade zingManager;
    [SerializeField] private SlowMoUpgrade slowMoManager;
    [SerializeField] private MoreComboUpgrade comboManager;
    [SerializeField] private IncomeIncreaseUpgrade incomeIncreaseManager;
    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private Button buyButton;

    private string currentUpgrade;

    private void Awake()
    {
        Instance=this;
        buyButton.onClick.AddListener(BuyUpgrade);
    }

    private void BuyUpgrade(){
        if(currentUpgrade=="MoreZing") zingManager.UpgradeMoreZing();
        else if(currentUpgrade=="MoreSlow") slowMoManager.UpgradeMoreSlow();
        else if(currentUpgrade=="MoreCombo") comboManager.UpgradeMoreCombo();
        else if(currentUpgrade=="IncreasedIncome") incomeIncreaseManager.UpgradeIncreaseIncome();
        else Debug.Log("currenly on nothing");
    }

    public void setCurrentUpgrade(string x)
    {
        currentUpgrade=x;
    }

    public void changeMoneyText(int x)
    {
        if(x!=-1) moneyText.text = "$"+x;
        else moneyText.text="maxed out";
    }

    public int getZingVal(){return zingManager.getZingVal();}
    public float getSlowMoVal(){return slowMoManager.getSlowMoVal();}
    public int getComboVal(){return comboManager.getComboVal();}
    public float getIncreasedIncomeVal(){return incomeIncreaseManager.getIncreasedIncomeVal();}

}
