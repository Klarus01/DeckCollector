using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopUIController shopUIController;
    [SerializeField] private ShopItem[] shopItems;
    [SerializeField] private ShopItem[] displayedItems;

    private void Start()
    {
        displayedItems = new ShopItem[shopUIController.shopButtons.Length];
        RefreshShopItems();
    }

    public void RefreshShopItems()
    {
        for (var i = 0; i < displayedItems.Length; i++)
        {
            var randomItem = GetRandomItem();
            displayedItems[i] = randomItem;
        }
        shopUIController.SetShopButtons(displayedItems);
    }

    private ShopItem GetRandomItem()
    {
        var randomIndex = Random.Range(0, shopItems.Length);
        var randomItem = shopItems[randomIndex];
        return randomItem;
    }

    public void ResetShop()
    {
        RefreshShopItems();
    }
}
