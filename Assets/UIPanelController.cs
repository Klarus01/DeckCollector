using UnityEngine;
using DG.Tweening;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private RectTransform upgradePanel;
    [SerializeField] private RectTransform shopPanel;
    [SerializeField] private float animationDuration = 0.5f;

    private Vector2 upgradeHidePosition;
    private Vector2 upgradeShowPosition;
    private Vector2 shopHidePosition;
    private Vector2 shopShowPosition;
    private bool isUpgradeShown;
    private bool isShopShown;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleUpgradePosition();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleShopPosition();
        }
    }

    private void Init()
    {
        upgradeShowPosition = upgradePanel.anchoredPosition;
        upgradeHidePosition = new Vector2(upgradeShowPosition.x - upgradePanel.rect.width, upgradeShowPosition.y);
        upgradePanel.anchoredPosition = upgradeHidePosition;

        shopShowPosition = shopPanel.anchoredPosition;
        shopHidePosition = new Vector2(shopShowPosition.x + shopPanel.rect.width, shopShowPosition.y);
        shopPanel.anchoredPosition = shopHidePosition;
    }

    private void ToggleUpgradePosition()
    {
        if (isUpgradeShown)
        {
            upgradePanel.DOAnchorPos(upgradeHidePosition, animationDuration).SetEase(Ease.InOutQuad);
        }
        else
        {
            upgradePanel.DOAnchorPos(upgradeShowPosition, animationDuration).SetEase(Ease.InOutQuad);
        }

        isUpgradeShown = !isUpgradeShown;
    }
    
    private void ToggleShopPosition()
    {
        if (isShopShown)
        {
            shopPanel.DOAnchorPos(shopHidePosition, animationDuration).SetEase(Ease.InOutQuad);
        }
        else
        {
            shopPanel.DOAnchorPos(shopShowPosition, animationDuration).SetEase(Ease.InOutQuad);
        }

        isShopShown = !isShopShown;
    }
}