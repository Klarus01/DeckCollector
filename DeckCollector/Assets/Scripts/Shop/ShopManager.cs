using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopUIController shopUIController;
    [SerializeField] private ShopItem[] shopItems;
    public ShopItem[] displayedItems;
    private readonly int refreshCost = 1;

    private void Start()
    {
        displayedItems = new ShopItem[shopUIController.shopButtons.Length];
        RefreshShopItems();
    }

    public void RefreshShopItems()
    {
        if (GameManager.Instance.GoldCount >= refreshCost)
        {
            GameManager.Instance.GoldCount -= refreshCost;
            for (int i = 0; i < displayedItems.Length; i++)
            {
                ShopItem randomItem = GetRandomItem();
                displayedItems[i] = randomItem;
            }
            shopUIController.SetShopButtons(displayedItems);
        }
    }

    public ShopItem GetRandomItem()
    {
        int randomIndex = Random.Range(0, shopItems.Length);
        ShopItem randomItem = shopItems[randomIndex];
        return randomItem;
    }
}
