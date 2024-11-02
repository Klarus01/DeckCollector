using UnityEngine;

public class SellCard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CardUI>(out var card))
        {
            card.isAboveSellPoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CardUI>(out var card))
        {
            card.isAboveSellPoint = false;
        }
    }
}