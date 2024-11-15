using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour
{
    [SerializeField] private Image unitImage;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text currentStatsText;
    [SerializeField] private TMP_Text nextStatsText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image[] upgradeStages;

    private Color originalColor;
    
    public Upgrade upgrade;
    public Upgrade.UpgradeLevel level;

    private void Start()
    {
        InitializeUpgradeUI();
        button.onClick.AddListener(OnButtonClick);
        currentStatsText.gameObject.SetActive(false);
        nextStatsText.gameObject.SetActive(false);
        originalColor = backgroundImage.color;
    }

    private void InitializeUpgradeUI()
    {
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        UpdateButtonText();
        ResetUpgradeStages();
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
        UpdateButtonText();
        OnMouseEnter();
    }

    private bool TryIfUpgradeIsPossible()
    {
        if (upgrade.upgradeLvl.Equals(upgrade.maxUpgradeLvl))
        {
            return false;
        }

        if (level.costForNextLvl > GameManager.Instance.PartCount)
        {
            return false;
        }

        return true;
    }

    private void OnMouseEnter()
    {
        var nextLvl = upgrade.upgradeLvl + 1;
        currentStatsText.gameObject.SetActive(true);
        currentStatsText.SetText($"Current:\r\nHP: {level.hp}\r\nDMG: {level.dmg}");
        
        if (upgrade.upgradeLvl.Equals(upgrade.maxUpgradeLvl)) return;
        
        costText.gameObject.SetActive(false);
        nextStatsText.gameObject.SetActive(true);
        nextStatsText.SetText($"Next lvl:\r\nHP: {upgrade.upgradeLevels[nextLvl].hp}\r\nDMG: {upgrade.upgradeLevels[nextLvl].dmg}");
    }

    private void OnMouseExit()
    {
        currentStatsText.gameObject.SetActive(false);
        nextStatsText.gameObject.SetActive(false);
        costText.gameObject.SetActive(true);
    }

    private void UpdateButtonText()
    {
        unitImage.sprite = upgrade.unitSprite;
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        
        if (upgrade.upgradeLvl.Equals(upgrade.maxUpgradeLvl))
        {
            costText.SetText($"MAX LEVEL");
        }
        else
        {
            costText.SetText($"Cost: {level.costForNextLvl}");
        }

        for (var i = 0; i < upgrade.upgradeLvl; i++)
        {
            upgradeStages[i].color = Color.green;
        }
    }


    public void ResetUpgradeStages()
    {
        foreach (var stage in upgradeStages)
        {
            stage.color = Color.white;
        }
        upgrade.upgradeLvl = 0;
        UpdateButtonText();
    }
    
    private IEnumerator ChangeBackgroundColorTemporarily(Color newColor)
    {
        backgroundImage.color = newColor;
        yield return new WaitForSeconds(.2f);
        backgroundImage.color = originalColor;
    }
}