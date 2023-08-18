using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text currentStatsText;
    [SerializeField] private TMP_Text nextStatsText;
    [SerializeField] private Image[] upgradeStages;

    public Upgrade upgrade;
    public Upgrade.UpgradeLevel level;

    private void Start()
    {
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        UpdateButtonText();
        button.onClick.AddListener(OnButtonClick);
        currentStatsText.gameObject.SetActive(false);
        nextStatsText.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (!TryIfUpgradeIsPossible())
        {
            return;
        }
        UpgradeManager.Instance.OnUpgradeBuy(upgrade);
        UpdateButtonText();
        OnMouseEnter();
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

    public void OnMouseEnter()
    {
        int nextLvl = upgrade.upgradeLvl + 1;
        currentStatsText.gameObject.SetActive(true);
        currentStatsText.SetText($"Current:\r\nHP: {level.hp}\r\nDMG: {level.dmg}");
        if (!upgrade.upgradeLvl.Equals(upgrade.maxUpgradeLvl))
        {
            costText.gameObject.SetActive(false);
            nextStatsText.gameObject.SetActive(true);
            nextStatsText.SetText($"Next lvl:\r\nHP: {upgrade.upgradeLevels[nextLvl].hp}\r\nDMG: {upgrade.upgradeLevels[nextLvl].dmg}");
        }
    }

    private void OnMouseExit()
    {
        currentStatsText.gameObject.SetActive(false);
        nextStatsText.gameObject.SetActive(false);
        costText.gameObject.SetActive(true);
    }

    public void UpdateButtonText()
    {
        image.sprite = upgrade.unitSprite;
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];

        if (upgrade.upgradeLvl.Equals(upgrade.maxUpgradeLvl))
        {
            costText.SetText($"MAX LEVEL");

        }
        else
        {
            costText.SetText($"Cost: {level.costForNextLvl}");
        }

        for (int i = 0; i < upgrade.upgradeLvl; i++)
        {
            upgradeStages[i].color = Color.green;
        }
    }
}