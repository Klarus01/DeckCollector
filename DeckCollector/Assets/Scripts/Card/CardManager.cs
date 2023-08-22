using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private CardUIController cardUIController;
    public Transform cardInUse;

    private void Start()
    {
        GameManager.Instance.OnHandUpdate += PrepareForNewDraw;
        PrepareForNewDraw();
    }

    private void PrepareForNewDraw()
    {
        cardUIController.PrepareForNewDraw(GameManager.Instance.deck.deck);
    }

    public void BackUnitToHand(Unit unit)
    {
        GameManager.Instance.deck.cardsInHand.Add(unit.unitData.unit);
        GameManager.Instance.deck.cardsOnBoard.Remove(unit);
        cardUIController.UnitBackToHand(unit);
    }

    public void NewCardBought(Unit unit)
    {
        GameManager.Instance.deck.cardsInHand.Add(unit.unitData.unit);
        cardUIController.UnitBackToHand(unit);
    }

    public void CollectAllCardsToHand()
    {
        List<Unit> cardsToMove = new(GameManager.Instance.deck.cardsOnBoard);

        foreach (Unit cardOnBoard in cardsToMove)
        {
            if (cardOnBoard.gameObject != null)
            {
                BackUnitToHand(cardOnBoard);
                Destroy(cardOnBoard.gameObject);
            }
        }
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