using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour
{
    [SerializeField] Upgrade upgrade;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private TMP_Text costText;

    Upgrade.UpgradeLevel level;

    private void Start()
    {
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        UpdateButtonText();
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (!TryIfUpgradeIsPossible())
        {
            return;
        }
        UpgradeManager.OnUpgradeBuy(upgrade);
        UpdateButtonText();
        GameManager.Instance.UpdateUI();
    }

    public bool TryIfUpgradeIsPossible()
    {
        if (upgrade.upgradeLvl.Equals(upgrade.maxUpgradeLvl))
        {
            return false;
        }

        if (level.costForNextLvl > GameManager.Instance.partsCount)
        {
            return false;
        }

        GameManager.Instance.partsCount -= level.costForNextLvl;
        return true;
    }

    public void UpdateButtonText()
    {
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        buttonText.text = upgrade.upgradeName + " (Lvl " + (upgrade.upgradeLvl + 1) + ")";
        costText.text = level.costForNextLvl.ToString();
    }
}