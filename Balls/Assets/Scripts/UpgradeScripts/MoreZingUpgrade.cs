using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class MoreZingUpgrade : MonoBehaviour
{

    private const string PLAYER_PREFS_MOREZINGUPGRADE = "MoreZingLevel";
    private Button button;

    [SerializeField] private List<Image> boxes;

    [SerializeField] private Sprite emptyBox;
    [SerializeField] private ParticlesPlayOnClickButton particlesPlayOnClickButton;

    private int moreZingUpgrade=0;
    public void UpgradeMoreZing()
    {
        // 100 - 110 - 120 - 130 - 140 - 150 - 160
        if(moreZingUpgrade==6){
            SoundManager.Instance.PlaySmallExplosion();
            particlesPlayOnClickButton.PlayEffects();
            return;
        }
        if (MoneyManager.Instance.getMoney() < moneyNeededToUpgrade(moreZingUpgrade))
        {
            Debug.Log("not enough momey!");
            return;
        }
        SoundManager.Instance.PlaySmallExplosion();
        particlesPlayOnClickButton.PlayEffects();
        MoneyManager.Instance.loseMoney(moneyNeededToUpgrade(moreZingUpgrade));
        Debug.Log("adding to zing total");
        moreZingUpgrade++;
        UpgradeManager.Instance.changeMoneyText(moneyNeededToUpgrade(moreZingUpgrade));
        UpdateBoxes(moreZingUpgrade);

        PlayerPrefs.SetInt(PLAYER_PREFS_MOREZINGUPGRADE, moreZingUpgrade);
    }

    private void Start()
    {
        UpdateBoxes(moreZingUpgrade);
    }

    private int getZingValWithMZU(int x)
    {
        int retval = x*20+100;
        // Debug.Log("returning "+retval+" cuz x="+x+", morezingupgrade="+moreZingUpgrade);
        return retval;
    }

    private void Awake()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        moreZingUpgrade=PlayerPrefs.GetInt(PLAYER_PREFS_MOREZINGUPGRADE);
    }

    private void OnButtonClicked()
    {
        SoundManager.Instance.PlayPopSound();
        UpgradeManager.Instance.changeMoneyText(moneyNeededToUpgrade(moreZingUpgrade));
        UpgradeManager.Instance.setCurrentUpgrade("MoreZing");
    }
    public void UpdateBoxes(int x)
    {
        if(x>6)Debug.Log("massive error");
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

    public int getZingVal() {return getZingValWithMZU(moreZingUpgrade);}
}
