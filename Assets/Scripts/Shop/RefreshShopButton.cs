using UnityEngine;
using UnityEngine.UI;

public class RefreshShopButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ShopManager shopManager;
    private readonly int refreshCost = 1;

    private void Start()
    {
        button.onClick.AddListener(RefreshShop);
    }

    public void RefreshShop()
    {
        if (GameManager.Instance.GoldCount >= refreshCost)
        {
            GameManager.Instance.GoldCount -= refreshCost;
            shopManager.RefreshShopItems();
        }
    }
}
