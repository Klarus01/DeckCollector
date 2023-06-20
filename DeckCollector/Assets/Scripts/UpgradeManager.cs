using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Upgrade[] upgrades;

    private void Awake()
    {
        CostSetUp();
    }

    public void UpgradePanelAnim()
    {
        animator.SetTrigger("ShowTrigger");
    }

    public void OnUpgradeBuy(Upgrade upgrade)
    {
        if (upgrade.upgradeLvl.Equals(upgrade.maxUpgradeLvl))
        {
            return;
        }

        if (upgrade.cost[upgrade.upgradeLvl] > GameManager.Instance.partsCount)
        {
            return;
        }

        GameManager.Instance.partsCount -= upgrade.cost[upgrade.upgradeLvl];

        UIManager.Instance.UpdateUI();

        if (upgrade.unit is Farmer)
        {
            Farmer[] farmers;
            farmers = FindObjectsOfType<Farmer>();
            upgrade.upgradeLvl++;
            foreach (Farmer f in farmers)
            {
                f.SetUpStats();
            }
            CostSetUp();
        }

        if (upgrade.unit is Knight)
        {
            Knight[] knight;
            knight = FindObjectsOfType<Knight>();
            upgrade.upgradeLvl++;
            foreach (Knight k in knight)
            {
                k.SetUpStats();
            }
            CostSetUp();
        }
    }

    private void CostSetUp()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            if (upgrade.id.Equals(0))
            {
                UIManager.Instance.costFarmer.text = upgrade.cost[upgrade.upgradeLvl].ToString();
            }
            else if (upgrade.id.Equals(1))
            {
                UIManager.Instance.costKnight.text = upgrade.cost[upgrade.upgradeLvl].ToString();
            }
        }
    }
}
