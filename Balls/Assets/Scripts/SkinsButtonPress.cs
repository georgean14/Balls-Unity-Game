using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsButtonPress : MonoBehaviour
{
    [SerializeField] private Button button;

    private int skinIdx;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
        if (CosmeticsManager.Instance.getSkinIdx() == skinIdx)
        {
            SkinsSubTextManager.Instance.SetText(CosmeticsManager.Instance.textsOfSkins[skinIdx]);
        }
    }

    private void OnButtonClicked()
    {
        CosmeticsMenuSoundManager.Instance.PlayPopSFX();
        if(!CosmeticsManager.Instance.lockStatusSkins[skinIdx]) {
            SkinsSubTextManager.Instance.SetText(CosmeticsManager.Instance.textsOfSkins[skinIdx]);
            CosmeticsManager.Instance.SelectSkin(skinIdx);
        }
        else
        {
            CosmeticsManager.Instance.setCurrentUnlockedSkinIdx(skinIdx);
            SkinsSubTextManager.Instance.SetBuy(CosmeticsManager.Instance.costOfSkins[skinIdx]);
            Debug.Log("locked");
        }
    }

    public void SetIndex(int idx)
    {
        skinIdx = idx;
    }
}
