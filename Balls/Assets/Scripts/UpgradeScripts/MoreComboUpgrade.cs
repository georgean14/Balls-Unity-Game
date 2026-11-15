using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreComboUpgrade : MonoBehaviour
{

    private const string PLAYER_PREFS_MORECOMBOUPGRADE = "Combo";
    private Button button;

    [SerializeField] private List<Image> boxes;

    [SerializeField] private Sprite emptyBox;
    [SerializeField] private ParticlesPlayOnClickButton particlesPlayOnClickButton;
    private int moreComboUpgrade=0;
    public void UpgradeMoreCombo()
    {
        // 100 - 110 - 120 - 130 - 140 - 150 - 160
        if(moreComboUpgrade==6){
            particlesPlayOnClickButton.PlayEffects();
            SoundManager.Instance.PlaySmallExplosion();
            return;
        }
        if (MoneyManager.Instance.getMoney() < moneyNeededToUpgrade(moreComboUpgrade))
        {
            Debug.Log("not enough momey!");
            return;
        }
        SoundManager.Instance.PlaySmallExplosion();
        particlesPlayOnClickButton.PlayEffects();
        MoneyManager.Instance.loseMoney(moneyNeededToUpgrade(moreComboUpgrade));
        moreComboUpgrade++;
        UpgradeManager.Instance.changeMoneyText(moneyNeededToUpgrade(moreComboUpgrade));
        UpdateBoxes(moreComboUpgrade);

        PlayerPrefs.SetInt(PLAYER_PREFS_MORECOMBOUPGRADE, moreComboUpgrade);
    }

    private void Start()
    {
        UpdateBoxes(moreComboUpgrade);
    }

    private int getComboValWithMCU(int x)
    {
        Debug.Log("HERE X="+x+" MCU = "+moreComboUpgrade);
        if(x==0)return 3;
        else if(x==1)return 4;
        else if(x==2)return 5;
        else if(x==3)return 8;
        else if(x==4)return 10;
        else if(x==5)return 12;
        else return 15;
    }

    private void Awake()
    {
        button=GetComponent<Button>();
        if(button!=null) {
            button.onClick.AddListener(OnButtonClicked);
            Debug.Log("button is not null");
        }
        else Debug.Log("button is null");
        moreComboUpgrade=PlayerPrefs.GetInt(PLAYER_PREFS_MORECOMBOUPGRADE);
        Debug.Log("SET MORE COMBO UPGRADE TO "+moreComboUpgrade);
    }

    private void OnButtonClicked()
    {
        SoundManager.Instance.PlayPopSound();
        UpgradeManager.Instance.changeMoneyText(moneyNeededToUpgrade(moreComboUpgrade));
        UpgradeManager.Instance.setCurrentUpgrade("MoreCombo");
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
        if(x==0) return 250;
        if(x==1) return 1000;
        if(x==2) return 3000;
        if(x==3) return 8000;
        if(x==4) return 20000;
        if(x==5) return 75000;
        else return -1;
    }

    public int getComboVal() {return getComboValWithMCU(moreComboUpgrade);}
}
