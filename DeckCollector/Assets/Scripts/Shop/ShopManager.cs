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
        for (int i = 0; i < displayedItems.Length; i++)
        {
            ShopItem randomItem = GetRandomItem();
            displayedItems[i] = randomItem;
        }
        shopUIController.SetShopButtons(displayedItems);
    }

    public ShopItem GetRandomItem()
    {
        int randomIndex = Random.Range(0, shopItems.Length);
        ShopItem randomItem = shopItems[randomIndex];
        return randomItem;
    }
}
