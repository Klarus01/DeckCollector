using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button slideButton;
    [SerializeField] private UpgradeButtonUI[] upgradeButtons;

    private void Start()
    {
        slideButton.onClick.AddListener(UpgradePanelAnim);
    }

    public void UpgradePanelAnim()
    {
        animator.SetTrigger("ShowTrigger");
    }
}