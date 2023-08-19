using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Deck deck;
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
        cardUIController.CardBackToHand(unit);
    }

    public void ToggleDropZone()
    {
        cardUIController.ToggleDropZone();
    }
}