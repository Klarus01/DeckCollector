using System.Collections.Generic;
using UnityEngine;

public class CardUIController : MonoBehaviour
{
    [SerializeField] private CardUI cardPrefab;
    [SerializeField] private GameObject cardHolder;
    [SerializeField] private List<CardUI> cards = new();

    public void PrepareForNewDraw(List<Unit> cardsToPlay, bool isNewHand = false)
    {
        DeleteCards();
        CreateHand(cardsToPlay, isNewHand);
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

    private void CreateHand(List<Unit> cardsToPlay, bool isNewHand = false)
    {
        foreach (var unit in cardsToPlay)
        {
            var cardUI = Instantiate(cardPrefab, cardHolder.transform);
            CreateCard(cardUI, unit, isNewHand);
        }
    }
    
    public void UnitBackToHand(Unit unit, bool isNewCard = false)
    {
        var cardUI = Instantiate(cardPrefab, cardHolder.transform);
        CreateCard(cardUI, unit, isNewCard);
    }

private void CreateCard(CardUI cardUI, Unit unit, bool isNewCard = false)
{
    cardUI.unit = unit.unitData.unit;
    cardUI.cardImage.sprite = unit.unitData.cardSprite;
    cardUI.unitMaxHealth = unit.upgrade.currentHelth;
    if (isNewCard) cardUI.unitHealth = cardUI.unitMaxHealth;
    else cardUI.unitHealth = unit.health;
    cards.Add(cardUI);
}

    public void SwitchCardHolderVisibility()
    {
        cardHolder.SetActive(!cardHolder.activeSelf);
    }
}