using TMPro;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text unitNameText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text dmgText;
    [SerializeField] private TMP_Text abilityText;

    private UnitData unit;

    public void Initialize(UnitData unitData)
    {
        unit = unitData;
        SetUpTexts();
    }

    private void SetUpTexts()
    {
        int nextLvl = GetNextLevel();
        unitNameText.SetText(unit.unitName);
        hpText.SetText($"HP:\n{unit.upgrade.currentHealth} -> {unit.upgrade.upgradeLevels[nextLvl].hp}");
        dmgText.SetText($"DMG:\n{unit.upgrade.currentDamage} -> {unit.upgrade.upgradeLevels[nextLvl].dmg}");
        abilityText.SetText($"{unit.abilityName}\n{unit.abilityDescription}");
    }

    private int GetNextLevel()
    {
        return unit.upgrade.upgradeLvl + 1;
    }
}