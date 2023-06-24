using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text goldText;
    public TMP_Text partsText;

    private void Start()
    {
        GameManager.Instance.OnUIUpdate += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnUIUpdate -= UpdateUI;
    }

    public void UpdateUI()
    {
        goldText.text = GameManager.Instance.goldCount.ToString();
        partsText.text = GameManager.Instance.partsCount.ToString();
    }
}
