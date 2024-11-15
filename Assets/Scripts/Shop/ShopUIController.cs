using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private UpgradeUIController upgradeUIController;
    [SerializeField] private Animator animator;
    [SerializeField] private Button slideButton;
    
    public bool isActive;
    public ShopButtonUI[] shopButtons;

    private void Start()
    {
        slideButton.onClick.AddListener(ShowShopPanel);
    }
    
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E)) return;
        ShowShopPanel();
        if(upgradeUIController.isActive) upgradeUIController.ShowUpgradePanelAnim();
    }

    public void ShowShopPanel()
    {
        isActive = !isActive;
        animator.SetTrigger("ShowTrigger");
    }

    public void SetShopButtons(ShopItem[] displayedItems)
    {
        for (var i = 0; i < displayedItems.Length; i++)
        {
            shopButtons[i].gameObject.SetActive(true);
            shopButtons[i].SetShopItem(displayedItems[i]);
        }
    }
}