using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ShopButtonUI[] shopButtons;
    [SerializeField] private ShopItem[] shopItems;
    public ShopItem[] displayedItems;
    private int refreshCost = 1;

    private void Start()
    {
        displayedItems = new ShopItem[shopButtons.Length];
        RefreshShopItems();
    }

    public void ShopPanelAnim()
    {
        animator.SetTrigger("ShowTrigger");
    }

    public void SetUpShopPanel()
    {
        for (int i = 0; i < displayedItems.Length; i++)
        {
            ShopItem item = displayedItems[i];
            shopButtons[i].gameObject.SetActive(true);
            shopButtons[i].SetShopItem(item);
        }
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
            SetUpShopPanel();
        }
    }

    public ShopItem GetRandomItem()
    {
        int randomIndex = Random.Range(0, shopItems.Length);
        ShopItem randomItem = shopItems[randomIndex];
        return randomItem;
    }
}
