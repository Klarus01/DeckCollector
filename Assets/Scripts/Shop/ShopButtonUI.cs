using UnityEngine;
using UnityEngine.UI;

public class ShopButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    private Unit unit;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void SetShopItem(ShopItem shopItem)
    {
        unit = shopItem.unit;
        image.sprite = shopItem.unitSprite;
    }

    public void OnButtonClick()
    {
        if (HasEnoughGold())
        {
            PerformCardPurchase();
        }
    }

    private bool HasEnoughGold()
    {
        return GameManager.Instance.GoldCount >= GameManager.Instance.ShopCost;
    }

    private void PerformCardPurchase()
    {
        GameManager.Instance.GoldCount -= GameManager.Instance.ShopCost;
        GameManager.Instance.ShopCost *= 2;
        GameManager.Instance.cardManager.NewCardBought(unit);
        gameObject.SetActive(false);
    }
}
