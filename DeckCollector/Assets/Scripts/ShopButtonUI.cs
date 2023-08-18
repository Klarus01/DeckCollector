using UnityEngine;
using UnityEngine.UI;

public class ShopButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    private Unit unit;

    private void Start()
    {
        button.onClick.AddListener(() => { OnButtonClick(unit); });
    }

    public void SetShopItem(ShopItem shopItem)
    {
        unit = shopItem.unit;
        image.sprite = shopItem.unitSprite;
    }

    public void OnButtonClick(Unit unit)
    {
        int cardCount = GameManager.Instance.deck.deck.Count;
        if (cardCount.Equals(GameManager.Instance.maxDeckSize))
        {
            return;
        }

        if (GameManager.Instance.GoldCount < GameManager.Instance.ShopCost)
        {
            return;
        }

        GameManager.Instance.GoldCount -= GameManager.Instance.ShopCost;
        GameManager.Instance.ShopCost *= 2;
        GameManager.Instance.deck.AddCard(unit);
        GameManager.Instance.UpdateHand();
        gameObject.SetActive(false);
    }
}
