using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefab;
    [SerializeField] private GameObject cardHolder;
    private List<GameObject> cards = new();
    private int numberOfCards;
    private float cardSpacing = 4f;

    private void Start()
    {
        numberOfCards = GameManager.Instance.deck.Count;
        for (int i = 0; i < numberOfCards; i++)
        {
            GameObject card = Instantiate(cardPrefab[GameManager.Instance.deck[i].id], transform);
            cards.Add(card);
        }

        ShuffleCards();

        ArrangeCards();
    }

    private void ShuffleCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = Random.Range(i, cards.Count);
            GameObject temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    private void ArrangeCards()
    {
        Vector3 cardHolderPosition = cardHolder.transform.position;

        float totalWidth = numberOfCards * cardSpacing;
        float startX = cardHolderPosition.x - (totalWidth / 2f);

        for (int i = 0; i < cards.Count; i++)
        {
            float xPosition = startX + i * cardSpacing;
            float yPosition = cardHolderPosition.y;
            Vector3 newPosition = new(xPosition, yPosition, cardHolderPosition.z);
            cards[i].transform.position = newPosition;
        }
    }


    public void ShiftCards()
    {
        cards.RemoveAt(0);

        ArrangeCards();
    }
}
