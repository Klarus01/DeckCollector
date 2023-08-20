using UnityEngine;
using UnityEngine.UI;

public class RefreshShopButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ShopManager shopManager;

    private void Start()
    {
        button.onClick.AddListener(RefreshShop);
    }

    public void RefreshShop()
    {
        shopManager.RefreshShopItems();
    }
}
