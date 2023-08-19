using System.Collections.Generic;
using UnityEngine;

public class CardUIController : MonoBehaviour
{
    [SerializeField] private CardUI cardPrefab;
    [SerializeField] private GameObject cardHolder;
    [SerializeField] private GameObject dropZone;
    private List<GameObject> cards = new();
    private bool isDropZoneActive = false;

    public void PrepareForNewDraw(List<Unit> cardsToPlay)
    {
        DeleteCards();
        CreateHand(cardsToPlay);
    }

    private void DeleteCards()
    {
        foreach (GameObject card in cards)
        {
            Destroy(card);
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

    public void CardBackToHand(Unit unit)
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
        cards.Add(cardUI.gameObject);
    }

    public void ToggleDropZone()
    {
        dropZone.SetActive(!dropZone.activeSelf);
        cardHolder.SetActive(!cardHolder.activeSelf);
        isDropZoneActive = !isDropZoneActive;
    }
}