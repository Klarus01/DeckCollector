using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionManager : SingletonMonobehaviour<CardSelectionManager>
{
    private float cardSpacing = 1f;
    
    public List<CardUI> selectedCards = new();

    public void ToggleCardSelection(CardUI card)
    {
        if (card.isSelected)
        {
            DeselectCard(card);
        }
        else
        {
            SelectCard(card);
        }
    }

    private void SelectCard(CardUI card)
    {
        card.isSelected = true;
        selectedCards.Add(card);
        card.HighlightCard(true);
    }

    private void DeselectCard(CardUI card)
    {
        card.isSelected = false;
        selectedCards.Remove(card);
        card.HighlightCard(false);
    }

    public void DragSelectedCards(Vector2 position)
    {
        foreach (var card in selectedCards)
        {
            card.transform.position = position;
        }
    }

    public void EndDragSelectedCards(CardUI referenceCard)
    {
        if (referenceCard.isAboveSellPoint)
        {
            SellSelectedCards();
        }
        else if (referenceCard.isAboveDropPoint)
        {
            ReturnAllSelectedCards();
        }
        else
        {
            PlaySelectedCards();
        }
    }

    public void EndDragSelectedCardsError()
    {
        ReturnAllSelectedCards(true);
    }

    private void SellSelectedCards()
    {
        var tempSelectedCards = new List<CardUI>(selectedCards);
        foreach (var card in tempSelectedCards)
        {
            GameManager.Instance.GoldCount += card.unit.cardValue;
            GameManager.Instance.ShopItemCost /= 1.25f;
            GameManager.Instance.deck.SellCard(card.unit, card);
            ClearSelection(card);
        }
    }

    private void PlaySelectedCards()
    {
        if (selectedCards.Count == 0)
            return;

        var tempSelectedCards = new List<CardUI>(selectedCards);
        var centerPosition = tempSelectedCards[0].transform.position;
        var radius = 1f;
        var angleIncrement = 360f / tempSelectedCards.Count;

        for (var i = 0; i < tempSelectedCards.Count; i++)
        {
            var card = tempSelectedCards[i];

            if (i == 0)
            {
                GameManager.Instance.deck.PlayCard(card.unit, centerPosition, card);
            }
            else
            {
                var angle = i * angleIncrement * Mathf.Deg2Rad;
                var xOffset = radius * Mathf.Cos(angle);
                var yOffset = radius * Mathf.Sin(angle);
                var cardPosition = new Vector3(centerPosition.x + xOffset, centerPosition.y + yOffset, 0);
                GameManager.Instance.deck.PlayCard(card.unit, cardPosition, card);
            }
            ClearSelection(card);
        }
    }

    private void ClearSelection(CardUI card)
    {
        card.isSelected = false;
        card.HighlightCard(false);
        selectedCards.Remove(card);
        Destroy(card.gameObject);
    }

    public void ReturnAllSelectedCards(bool isStillSelected = false)
    {
        var tempSelectedCards = new List<CardUI>(selectedCards);
        foreach (var card in tempSelectedCards)
        {
            card.isDragged = false;
            card.transform.SetParent(card.originalParent);
            card.transform.position = card.originalPosition;
            if(!isStillSelected) DeselectCard(card);
            LayoutRebuilder.ForceRebuildLayoutImmediate(card.originalParent.GetComponent<RectTransform>());
        }
        if(!isStillSelected) selectedCards.Clear();
    }
}
