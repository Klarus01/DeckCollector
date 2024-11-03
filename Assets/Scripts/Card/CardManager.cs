using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public CardUIController cardUIController;
    public Transform cardInUse;

    private void Start()
    {
        GameManager.Instance.OnHandUpdate += PrepareForNewDraw;
        PrepareForNewDraw();
    }
    
    public void ResetHand()
    {
        CollectAllCardsToHand();
        GameManager.Instance.deck.cardsInHand.Clear();

        foreach (var unit in GameManager.Instance.deck.startingUnits)
        {
            GameManager.Instance.deck.cardsInHand.Add(unit);
        }

        PrepareForNewDraw();
    }
    
    private void PrepareForNewDraw()
    {
        cardUIController.PrepareForNewDraw(GameManager.Instance.deck.cardsInHand);
    }

    public void CardPlayed(CardUI card)
    {
        cardUIController.CardPlayed(card);
    }

    public void CardSold(CardUI card)
    {
        cardUIController.CardPlayed(card);
    }

    public void BackUnitToHand(Unit unit)
    {
        GameManager.Instance.deck.cardsInHand.Add(unit.unitData.unit);
        GameManager.Instance.deck.cardsOnBoard.Remove(unit);
        cardUIController.UnitBackToHand(unit);
    }

    public void NewCardBought(Unit unit)
    {
        GameManager.Instance.deck.AddCard(unit);
        cardUIController.UnitBackToHand(unit);
    }

    public void CollectAllCardsToHand()
    {
        var cardsToMove = new List<Unit>(GameManager.Instance.deck.cardsOnBoard);

        foreach (var cardOnBoard in cardsToMove)
        {
            if (cardOnBoard != null && cardOnBoard.gameObject != null)
            {
                BackUnitToHand(cardOnBoard);
                Destroy(cardOnBoard.gameObject);
            }
        }

        var tombstoneToMove = new List<Tombstone>(GameManager.Instance.deck.cardsAsTombstone);

        foreach (var tombStone in tombstoneToMove)
        {
            BackUnitToHand(tombStone.originalUnit);
            Destroy(tombStone.gameObject);
        }

        GameManager.Instance.deck.cardsAsTombstone.Clear();
    }

    public void DropZoneOn()
    {
        cardUIController.DropZoneOn();
    }

    public void DropZoneOff()
    {
        cardUIController.DropZoneOff();
    }
}