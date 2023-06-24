using UnityEngine;
using UnityEngine.UI;

public class UpgradeManagerUI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button sliderButton;
    [SerializeField] private UpgradeButtonUI[] upgradeButtons;

    private void Start()
    {
        sliderButton.onClick.AddListener(UpgradePanelAnim);
    }

    public void UpgradePanelAnim()
    {
        animator.SetTrigger("ShowTrigger");
    }
}