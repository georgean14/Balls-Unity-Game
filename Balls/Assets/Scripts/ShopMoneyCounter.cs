using System;
using UnityEngine;
using TMPro;

public class ShopMoneyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText.text="$"+ MoneyManager.Instance.getMoney();
        MoneyManager.Instance.OnMoneyChanged+=moneyChanged;
    }

    private void moneyChanged(object sender,EventArgs e)
    {
        moneyText.text="$"+ MoneyManager.Instance.getMoney();
    }

}
