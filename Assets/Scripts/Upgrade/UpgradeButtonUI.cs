using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour
{
    [SerializeField] private Image unitImage;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text costText;
    //[SerializeField] private TMP_Text currentStatsText;
    //[SerializeField] private TMP_Text nextStatsText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image[] upgradeStages;
    [SerializeField] private string costKey = "UpgradeCost";
    [SerializeField] private string maxCostKey = "MaxCost";

    private Color originalColor;
    
    public Upgrade upgrade;
    public Upgrade.UpgradeLevel level;

    private void Start()
    {
        InitializeUpgradeUI();
        button.onClick.AddListener(OnButtonClick);
        //currentStatsText.gameObject.SetActive(false);
        //nextStatsText.gameObject.SetActive(false);
        originalColor = backgroundImage.color;
        LocalizationSettings.SelectedLocaleChanged += OnLanguageChanged;
    }

    private void InitializeUpgradeUI()
    {
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        UpdateButton();
        ResetUpgradeStages();
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged(UnityEngine.Localization.Locale obj)
    {
        UpdateButtonText();
    }

    private void OnButtonClick()
    {
        if (!TryIfUpgradeIsPossible())
        {
            StartCoroutine(ChangeBackgroundColorTemporarily(new Color(0.86f, 0.2f, 0.2f, 1f)));
            return;
        }
        StartCoroutine(ChangeBackgroundColorTemporarily(new Color(0.1f, 0.5f, 0.1f, 1f)));

        GameManager.Instance.PartCount -= level.costForNextLvl;
        UpgradeManager.Instance.OnUpgradeBuy(upgrade);
        UpdateButton();
    }

    private bool TryIfUpgradeIsPossible()
    {
        if (upgrade.upgradeLvl.Equals(upgrade.maxUpgradeLvl))
        {
            return false;
        }

        return level.costForNextLvl <= GameManager.Instance.PartCount;
    }

    private void UpdateButton()
    {
        unitImage.sprite = upgrade.unitSprite;
        
        for (var i = 0; i < upgrade.upgradeLvl; i++)
        {
            upgradeStages[i].color = Color.green;
        }

        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        
        if (upgrade.upgradeLvl >= upgrade.maxUpgradeLvl)
        {
            var localizedMaxLevel = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("SidePanels", maxCostKey);
            localizedMaxLevel.Completed += handle =>
            {
                costText.SetText(handle.Result);
            };
        }
        else
        {
            var localizedCost = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("SidePanels", costKey, new object[] { level.costForNextLvl});
            localizedCost.Completed += handle =>
            {
                costText.SetText(handle.Result);
            };
        }
    }

    public void ResetUpgradeStages()
    {
        foreach (var stage in upgradeStages)
        {
            stage.color = Color.white;
        }
        upgrade.upgradeLvl = 0;
        UpdateButton();
    }

    private IEnumerator ChangeBackgroundColorTemporarily(Color newColor)
    {
        backgroundImage.color = newColor;
        yield return new WaitForSeconds(.2f);
        backgroundImage.color = originalColor;
    }
}