using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionManager : SingletonMonobehaviour<CardSelectionManager>
{
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
        var tempSelectedCards = new List<CardUI>(selectedCards);
        foreach (var card in tempSelectedCards)
        {
            GameManager.Instance.deck.PlayCard(card.unit, card.transform, card);
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
