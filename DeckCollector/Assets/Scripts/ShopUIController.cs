using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button slideButton;
    public ShopButtonUI[] shopButtons;

    private void Start()
    {
        slideButton.onClick.AddListener(ShowShopPanel);
    }

    public void ShowShopPanel()
    {
        animator.SetTrigger("ShowTrigger");
    }

    public void SetShopButtons(ShopItem[] displayedItems)
    {
        for (int i = 0; i < displayedItems.Length; i++)
        {
            shopButtons[i].gameObject.SetActive(true);
            shopButtons[i].SetShopItem(displayedItems[i]);
        }
    }
}