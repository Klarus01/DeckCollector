using UnityEngine;
using UnityEngine.UI;

public class CampfireUIController : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button bagButton;

    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject bagPanel;

    private void Start()
    {
        upgradeButton.onClick.AddListener(OpenUpgradePanel);
        shopButton.onClick.AddListener(OpenShopPanel);
        bagButton.onClick.AddListener(OpenBagPanel);
    }

    private void OpenUpgradePanel()
    {
        bagPanel.SetActive(false);
        shopPanel.SetActive(false);
        upgradePanel.SetActive(true);
    }

    private void OpenShopPanel()
    {
        upgradePanel.SetActive(false);
        bagPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    private void OpenBagPanel()
    {
        upgradePanel.SetActive(false);
        shopPanel.SetActive(false);
        bagPanel.SetActive(true);
    }
}