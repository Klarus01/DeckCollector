using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image unitImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text unitName;
    [SerializeField] private TMP_Text unitStats;

    private Unit unit;
    private Color originalColor;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        originalColor = backgroundImage.color;
    }

    public void SetShopItem(ShopItem shopItem)
    {
        unit = shopItem.unit;
        unitImage.sprite = shopItem.unitSprite;
        unitName.SetText(unit.name); 
        UpdateItemText();
    }

    private void OnButtonClick()
    {
        if (!HasEnoughGold())
        {
            StartCoroutine(ChangeBackgroundColorTemporarily(new Color(0.86f, 0.2f, 0.2f, 1f)));
            return;
        }
        
        PerformCardPurchase();
    }

    public void UpdateItemText()
    {
        unitStats.SetText($"ATK: {unit.unitData.damage}\nHP: {unit.unitData.maxHealth}");
    }

    private bool HasEnoughGold()
    {
        return GameManager.Instance.GoldCount >= (int)GameManager.Instance.ShopItemCost;
    }

    private void PerformCardPurchase()
    {
        GameManager.Instance.GoldCount -= (int)GameManager.Instance.ShopItemCost;
        GameManager.Instance.ShopItemCost *= 1.25f;
        CardManager.Instance.NewCardBought(unit);
        gameObject.SetActive(false);
    }
    
    private IEnumerator ChangeBackgroundColorTemporarily(Color newColor)
    {
        backgroundImage.color = newColor;
        yield return new WaitForSeconds(.2f);
        backgroundImage.color = originalColor;
    }
}
