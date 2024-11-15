using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static GameManager;

public class ShopButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image unitImage;
    [SerializeField] private Image backgroundImage;

    private Unit unit;
    private Color originalColor;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        originalColor = unitImage.color;
    }

    public void SetShopItem(ShopItem shopItem)
    {
        unit = shopItem.unit;
        unitImage.sprite = shopItem.unitSprite;
    }

    public void OnButtonClick()
    {
        if (!HasEnoughGold())
        {
            StartCoroutine(ChangeBackgroundColorTemporarily(new Color(0.86f, 0.2f, 0.2f, 1f)));
            return;
        }
        
        StartCoroutine(ChangeBackgroundColorTemporarily(new Color(0.1f, 0.5f, 0.1f, 1f)));
        PerformCardPurchase();
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
