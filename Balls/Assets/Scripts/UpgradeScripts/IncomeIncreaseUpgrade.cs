using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomeIncreaseUpgrade : MonoBehaviour
{
    
    private const string PLAYER_PREFS_INCREASEINCOMEUPGRADE = "IncreaseIncome";
    private Button button;

    [SerializeField] private List<Image> boxes;

    [SerializeField] private Sprite emptyBox;
    [SerializeField] private ParticlesPlayOnClickButton particlesPlayOnClickButton;
    private int increaseIncomeUpgrade=0;
    public void UpgradeIncreaseIncome()
    {
        // 100 - 110 - 120 - 130 - 140 - 150 - 160
        if(increaseIncomeUpgrade==6){
            particlesPlayOnClickButton.PlayEffects();
            SoundManager.Instance.PlaySmallExplosion();
            return;
        }
        if (MoneyManager.Instance.getMoney() < moneyNeededToUpgrade(increaseIncomeUpgrade))
        {
            Debug.Log("not enough momey!");
            return;
        }
        particlesPlayOnClickButton.PlayEffects();
        SoundManager.Instance.PlaySmallExplosion();
        MoneyManager.Instance.loseMoney(moneyNeededToUpgrade(increaseIncomeUpgrade));
        increaseIncomeUpgrade++;
        UpgradeManager.Instance.changeMoneyText(moneyNeededToUpgrade(increaseIncomeUpgrade));
        UpdateBoxes(increaseIncomeUpgrade);

        PlayerPrefs.SetInt(PLAYER_PREFS_INCREASEINCOMEUPGRADE, increaseIncomeUpgrade);
    }

    private void Start()
    {
        UpdateBoxes(increaseIncomeUpgrade);
        Debug.Log("just started:"+increaseIncomeUpgrade);
    }

    private float getIncreaseValueWithIIU(int x)
    {
        if(x==0)return 1f;
        if(x==1)return 1.5f;
        if(x==2)return 2.1f;
        if(x==3)return 2.9f;
        if(x==4)return 5.3f;
        if(x==5)return 8.5f;
        else return 11.2f;
    }

    private void Awake()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        increaseIncomeUpgrade=PlayerPrefs.GetInt(PLAYER_PREFS_INCREASEINCOMEUPGRADE);
    }

    private void OnButtonClicked()
    {
        SoundManager.Instance.PlayPopSound();
        UpgradeManager.Instance.changeMoneyText(moneyNeededToUpgrade(increaseIncomeUpgrade));
        UpgradeManager.Instance.setCurrentUpgrade("IncreasedIncome");
    }
    public void UpdateBoxes(int x)
    {
        for(int i = 0; i < x; i++)
        {
            boxes[i].sprite=null;
        }
        for(int i = x; i < 6; i++)
        {
            boxes[i].sprite=emptyBox;
        }
    }
    private int moneyNeededToUpgrade(int x)
    {
        // 250 -> 500 -> 1000 -> 2000 -> 3000 -> 5000
        if(x==0) return 500;
        if(x==1) return 1000;
        if(x==2) return 5000;
        if(x==3) return 12000;
        if(x==4) return 50000;
        if(x==5) return 100000;
        else return -1;
    }

    public float getIncreasedIncomeVal() {
        Debug.Log("tryna return som:"+increaseIncomeUpgrade);
        return getIncreaseValueWithIIU(increaseIncomeUpgrade);
    }
}
