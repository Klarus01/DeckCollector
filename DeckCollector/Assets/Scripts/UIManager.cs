using TMPro;

public class UIManager : SingletonMonobehaviour<UIManager>
{
    public TMP_Text goldText;
    public TMP_Text partsText;
    public TMP_Text costFarmer;
    public TMP_Text costKnight;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        goldText.text = GameManager.Instance.goldCount.ToString();
        partsText.text = GameManager.Instance.partsCount.ToString();
    }
}
