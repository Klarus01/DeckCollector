using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image unitImage;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject unitInfoPanel;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image[] upgradeStages;

    private Color originalColor;
    public UnityEvent onUnitUpgraded;

    public Upgrade upgrade;
    public Upgrade.UpgradeLevel level;

    private void Start()
    {
        InitializeUpgradeUI();
        button.onClick.AddListener(OnButtonClick);
        originalColor = backgroundImage.color;
    }

    private void InitializeUpgradeUI()
    {
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        UpdateButton();
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

        GameManager.Instance.PartCount -= level.costForNextLvl;
        UpgradeManager.Instance.OnUpgradeBuy(upgrade);
        onUnitUpgraded?.Invoke();

        UpdateButton();
        UpdateButtonText();
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
    }

    private void UpdateButtonText()
    {
        level = upgrade.upgradeLevels[upgrade.upgradeLvl];
        
        if (upgrade.upgradeLvl >= upgrade.maxUpgradeLvl)
        {
            costText.SetText("MAXED!");
        }
        else
        {
            costText.SetText($"Cost: {level.costForNextLvl}");
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        unitInfoPanel.SetActive(true);
        unitInfoPanel.GetComponent<UnitInfo>().Initialize(upgrade.unit.unitData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        unitInfoPanel.SetActive(false);
    }

    private IEnumerator ChangeBackgroundColorTemporarily(Color newColor)
    {
        backgroundImage.color = newColor;
        yield return new WaitForSeconds(.2f);
        backgroundImage.color = originalColor;
    }
}