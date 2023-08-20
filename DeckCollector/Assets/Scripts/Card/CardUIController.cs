using System.Collections.Generic;
using UnityEngine;

public class CardUIController : MonoBehaviour
{
    [SerializeField] private CardUI cardPrefab;
    [SerializeField] private GameObject cardHolder;
    [SerializeField] private GameObject dropZone;
    [SerializeField] private List<CardUI> cards = new();
    private bool isDropZoneActive = false;

    public void PrepareForNewDraw(List<Unit> cardsToPlay)
    {
        DeleteCards();
        CreateHand(cardsToPlay);
    }

    private void DeleteCards()
    {
        foreach (CardUI cardUI in cards)
        {
            Destroy(cardUI);
        }
        cards.Clear();
    }

    private void CreateHand(List<Unit> cardsToPlay)
    {
        foreach (Unit unit in cardsToPlay)
        {
            CardUI cardUI = Instantiate(cardPrefab, cardHolder.transform);
            CreateCard(cardUI, unit);
        }
    }

    public void UnitBackToHand(Unit unit)
    {
        CardUI cardUI = Instantiate(cardPrefab, cardHolder.transform);
        CreateCard(cardUI, unit);
    }

    private void CreateCard(CardUI cardUI, Unit unit)
    {
        cardUI.unit = unit.unitData.unit;
        cardUI.cardImage.sprite = unit.unitData.cardSprite;
        cardUI.unitHealth = unit.health;
        cardUI.unitMaxHealth = unit.maxHealth;
        cards.Add(cardUI);
    }

    public void ToggleDropZone()
    {
        dropZone.SetActive(!dropZone.activeSelf);
        cardHolder.SetActive(!cardHolder.activeSelf);
        isDropZoneActive = !isDropZoneActive;
    }
}