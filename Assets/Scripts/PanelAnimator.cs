using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PanelAnimator : MonoBehaviour
{
    [Header("Panel Settings")]
    public RectTransform panel;
    public Button slideButton;
    public bool isLeftPanel = true;

    [Header("Animation Settings")]
    public float animationDuration = 0.3f;

    private bool isVisible = false;
    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;

    void Start()
    {
        // Ustalamy pozycjê widoczn¹ (obecna pozycja panelu)
        visiblePosition = panel.anchoredPosition;

        // Ustalamy pozycjê ukryt¹ poza ekranem
        float hiddenX = isLeftPanel ? -panel.rect.width : Screen.width + panel.rect.width;
        hiddenPosition = new Vector2(hiddenX, panel.anchoredPosition.y);

        // Ustawiamy panel w pozycji ukrytej na starcie
        panel.anchoredPosition = hiddenPosition;
        slideButton.onClick.AddListener(TogglePanel);
    }

    public void TogglePanel()
    {
        if (isVisible)
        {
            // Chowanie panelu
            panel.DOAnchorPos(hiddenPosition, animationDuration);
        }
        else
        {
            // Wysuwanie panelu
            panel.DOAnchorPos(visiblePosition, animationDuration);
        }

        isVisible = !isVisible;
    }
}