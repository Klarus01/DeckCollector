using System.Collections.Generic;
using UnityEngine;

public class CardUIController : MonoBehaviour
{
    [SerializeField] private CardUI cardPrefab;
    [SerializeField] private GameObject cardHolder;
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

    public void CardSold(CardUI card)
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
    }

    private void CreateCard(CardUI cardUI, Unit unit)
    {
        cardUI.unit = unit.unitData.unit;
        cardUI.cardImage.sprite = unit.unitData.cardSprite;
        cardUI.unitHealth = unit.health;
        cardUI.unitMaxHealth = unit.unitData.maxHealth;
        cards.Add(cardUI);
    }

    public void SwitchCardHolderVisibility()
    {
        cardHolder.SetActive(!cardHolder.activeSelf);
    }
}