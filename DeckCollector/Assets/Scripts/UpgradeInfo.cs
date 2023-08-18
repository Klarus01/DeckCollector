using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfo : MonoBehaviour
{
    public Image image;
    public TMP_Text infoText;

    private void OnMouseEnter()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;

        Vector3 textPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        image.rectTransform.position = textPosition;

        if (TryGetComponent<UpgradeButtonUI>(out UpgradeButtonUI upgrade))
        {
            int nextLvl = upgrade.upgrade.upgradeLvl + 1;

            infoText.SetText($"Current stats:\nHP: {upgrade.level.hp}\nDMG: {upgrade.level.dmg}" +
                $"\n\nNext level:\nHP: {upgrade.upgrade.upgradeLevels[nextLvl].hp}\nDMG: {upgrade.upgrade.upgradeLevels[nextLvl].dmg}");

            image.gameObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        image.gameObject.SetActive(false);
    }
}
