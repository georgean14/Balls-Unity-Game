using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMoUpgrade : MonoBehaviour
{
    private const string PLAYER_PREFS_SLOWMOUPGRADE = "SlowMoLevel";
    private Button button;

    [SerializeField] private List<Image> boxes;

    [SerializeField] private Sprite emptyBox;
    [SerializeField] private ParticlesPlayOnClickButton particlesPlayOnClickButton;
    private int slowMoUpgrade=0;
    public void UpgradeMoreSlow()
    {
        // 100 - 110 - 120 - 130 - 140 - 150 - 160
        if(slowMoUpgrade==6){
            SoundManager.Instance.PlaySmallExplosion();
            particlesPlayOnClickButton.PlayEffects();
            return;
        }
        if (MoneyManager.Instance.getMoney() < moneyNeededToUpgrade(slowMoUpgrade))
        {
            Debug.Log("not enough momey!");
            return;
        }
        SoundManager.Instance.PlaySmallExplosion();
        particlesPlayOnClickButton.PlayEffects();
        MoneyManager.Instance.loseMoney(moneyNeededToUpgrade(slowMoUpgrade));
        slowMoUpgrade++;
        UpgradeManager.Instance.changeMoneyText(moneyNeededToUpgrade(slowMoUpgrade));
        UpdateBoxes(slowMoUpgrade);

        PlayerPrefs.SetInt(PLAYER_PREFS_SLOWMOUPGRADE, slowMoUpgrade);
    }

    private void Start()
    {
        UpdateBoxes(slowMoUpgrade);
    }

    private float getSlowValWithSMU(int x)
    {
        if(x==0)return 0.4f;
        if(x==1)return 0.3f;
        if(x==2)return 0.2f;
        if(x==3)return 0.15f;
        if(x==4)return 0.1f;
        if(x==5)return 0.05f;
        else return 0.05f;
    }

    private void Awake()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        slowMoUpgrade=PlayerPrefs.GetInt(PLAYER_PREFS_SLOWMOUPGRADE);
    }

    private void OnButtonClicked()
    {
        SoundManager.Instance.PlayPopSound();
        UpgradeManager.Instance.changeMoneyText(moneyNeededToUpgrade(slowMoUpgrade));
        UpgradeManager.Instance.setCurrentUpgrade("MoreSlow");
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

    public float getSlowMoVal() {return getSlowValWithSMU(slowMoUpgrade);}
}
