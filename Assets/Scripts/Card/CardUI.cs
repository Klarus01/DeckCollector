using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Color originalColor;
    private float restTimer;
    private Vector2 initialMousePosition;
    private const float dragThreshold = 20f;
    private bool isHighlighted;
    
    public Image cardImage;
    public Unit unit;
    public float unitHealth;
    public float unitMaxHealth;
    public Transform originalParent;
    public Vector3 originalPosition;
    public Slider slider;
    public bool isSelected;
    public bool isDragged;
    public bool isAboveSellPoint;
    public bool isAboveDropPoint;

    private void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalColor = cardImage.color;

        RestTimeCalculation();
        SetRestSlider();
    }

    private void Update()
    {
        if (restTimer > 0f)
        {
            restTimer -= Time.deltaTime;
            slider.value = restTimer;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CardSelectionManager.Instance.ReturnAllSelectedCards();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (restTimer > 0f) return;
        if (isDragged) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            CardSelectionManager.Instance.ToggleCardSelection(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (restTimer > 0f) return;

        initialMousePosition = eventData.position;
        
        if (!isSelected)
        {
            CardSelectionManager.Instance.ToggleCardSelection(this);
        }

        isDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Vector2.Distance(initialMousePosition, eventData.position) < dragThreshold)
        {
            return;
        }
        
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CardSelectionManager.Instance.DragSelectedCards(mousePos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Vector2.Distance(initialMousePosition, eventData.position) < dragThreshold)
        {
            CardSelectionManager.Instance.EndDragSelectedCardsError();
            return;
        }
        
        isDragged = false;
        CardSelectionManager.Instance.EndDragSelectedCards(this);
    }

    public void HighlightCard(bool highlight)
    {
        isHighlighted = highlight;
        cardImage.color = isHighlighted ? new Color(1f, 0.4f, 0.9f, 1f) : originalColor;
    }

    private void SetRestSlider()
    {
        slider.maxValue = restTimer;
        slider.value = restTimer;
    }

    private void RestTimeCalculation()
    {
        restTimer = (unitMaxHealth - unitHealth) * 0.75f;
        if (unitHealth.Equals(0))
        {
            restTimer *= 1.5f;
        }
        if (!unitMaxHealth.Equals(unitHealth)) FlashGold();
    }

    private void FlashGold()
    {
        StartCoroutine(FlashGoldCoroutine());
    }

    private IEnumerator FlashGoldCoroutine()
    {
        while (restTimer > 0)
        {
            yield return new WaitForSeconds(restTimer);
        }
        cardImage.color = new Color(1f, 0.92f, 0.016f, 1f);
        yield return new WaitForSeconds(0.5f);
        cardImage.color = isHighlighted ? new Color(1f, 0.4f, 0.9f, 1f) : originalColor;
    }
}