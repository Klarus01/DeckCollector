using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Unit> cardsInHand = new();
    public List<Unit> cardsOnBoard = new();
    public List<Tombstone> cardsAsTombstone = new();
    public List<Unit> startingUnits = new();
    
    private void Awake()
    {
        cardsInHand.AddRange(startingUnits);
    }
    
    public void PlayCard(Unit unit, Transform trans, CardUI card)
    {
        var newUnit = Instantiate(unit, trans.position, Quaternion.identity);
        cardsOnBoard.Add(newUnit);
        cardsInHand.Remove(unit);
        CardManager.Instance.CardPlayed(card);
    }

    public void AddCard(Unit unit)
    {
        cardsInHand.Add(unit);
    }

    public void SellCard(Unit unit, CardUI card)
    {
        cardsInHand.Remove(unit);
        CardManager.Instance.CardSold(card);
    }
}