using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefab;
    [SerializeField] private GameObject cardHolder;
    [SerializeField] private List<GameObject> cards = new();
    private List<Unit> cardsToPlay = new();

    private void Start()
    {
        cardsToPlay = GameManager.Instance.deck.deck;
        GameManager.Instance.OnHandUpdate += PrepareForNewDraw;

        PrepareForNewDraw();
    }

    private void PrepareForNewDraw()
    {
        DeleteCards();
        CreateCards();
        ShuffleCards();
    }

    private void DeleteCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        cards.Clear();
    }

    private void CreateCards()
    {
        //zszufluj tutaj, beka
        for (int i = 0; i < cardsToPlay.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab[cardsToPlay[i].id], cardHolder.transform);
            cards.Add(card);
        }
        cardsToPlay = GameManager.Instance.deck.cardsInHand;
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
}