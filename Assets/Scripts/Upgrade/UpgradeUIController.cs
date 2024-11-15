using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIController : MonoBehaviour
{
    [SerializeField] private ShopUIController shopUIController;
    [SerializeField] private Animator animator;
    [SerializeField] private Button slideButton;
    [SerializeField] private UpgradeButtonUI[] upgradeButtons;

    public bool isActive;

    private void Start()
    {
        slideButton.onClick.AddListener(ShowUpgradePanelAnim);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;
        ShowUpgradePanelAnim();
        if(shopUIController.isActive) shopUIController.ShowShopPanel();
    }

    public void ShowUpgradePanelAnim()
    {
        isActive = !isActive;
        animator.SetTrigger("ShowTrigger");
    }
}