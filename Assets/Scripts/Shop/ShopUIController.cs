using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    public ShopButtonUI[] shopButtons;

    public void UpdateUnitStats()
    {
        foreach(ShopButtonUI button in shopButtons)
        {
            button.UpdateItemText();
        }
    }

    public void SetShopButtons(ShopItem[] displayedItems)
    {
        for (var i = 0; i < displayedItems.Length; i++)
        {
            shopButtons[i].gameObject.SetActive(true);
            shopButtons[i].SetShopItem(displayedItems[i]);
        }
    }
}