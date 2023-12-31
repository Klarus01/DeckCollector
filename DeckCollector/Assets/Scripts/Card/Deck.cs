using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Unit> cardsInHand = new();
    public List<Unit> cardsOnBoard = new();
    public List<Unit> deck = new();

    private void Start()
    {
        cardsInHand.AddRange(deck);
    }

    public void PlayCard(Unit unit, Transform trans, CardUI card)
    {
        Unit newUnit = Instantiate(unit, trans.position, Quaternion.identity);
        cardsOnBoard.Add(newUnit);
        cardsInHand.Remove(unit);
        GameManager.Instance.cardManager.CardPlayed(card);
    }

    public void AddCard(Unit unit)
    {
        cardsInHand.Add(unit);
        deck.Add(unit);
    }

    public void SellCard(Unit unit)
    {
        deck.Remove(unit);
        cardsInHand.Remove(unit);
    }
}
