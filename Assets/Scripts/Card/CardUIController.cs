using System.Collections.Generic;
using UnityEngine;

public class CardUIController : MonoBehaviour
{
    [SerializeField] private CardUI cardPrefab;
    [SerializeField] private GameObject cardHolder;
    [SerializeField] private GameObject dropZone;
    [SerializeField] private List<CardUI> cards = new();

    public void PrepareForNewDraw(List<Unit> cardsToPlay)
    {
        DeleteCards();
        CreateHand(cardsToPlay);
    }

    public void CardPlayed(CardUI card)
    {
        cards.Remove(card);
    }

    private void DeleteCards()
    {
        foreach (var cardUI in cards)
        {
            Destroy(cardUI.gameObject);
        }
        cards.Clear();
    }

    private void CreateHand(List<Unit> cardsToPlay)
    {
        foreach (var unit in cardsToPlay)
        {
            var cardUI = Instantiate(cardPrefab, cardHolder.transform);
            CreateCard(cardUI, unit);
        }
    }

    public void UnitBackToHand(Unit unit)
    {
        var cardUI = Instantiate(cardPrefab, cardHolder.transform);
        CreateCard(cardUI, unit);
        DropZoneOff();
    }

    private void CreateCard(CardUI cardUI, Unit unit)
    {
        cardUI.unit = unit.unitData.unit;
        cardUI.cardImage.sprite = unit.unitData.cardSprite;
        cardUI.unitHealth = unit.health;
        cardUI.unitMaxHealth = unit.maxHealth;
        cards.Add(cardUI);
    }

    public void DropZoneOff()
    {
        dropZone.SetActive(false);
        cardHolder.SetActive(true);
    }

    public void DropZoneOn()
    {
        dropZone.SetActive(true);
        cardHolder.SetActive(false);
    }
}