using UnityEngine;

public class CardDropZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CardUI>(out CardUI card))
        {
            card.isAboveDropPoint = true;
        }
        else if (collision.TryGetComponent<Unit>(out Unit unit))
        {
            unit.Stats.ToggleIsAboveDropPoint(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CardUI>(out CardUI card))
        {
            card.isAboveDropPoint = false;
        }
        else if (collision.TryGetComponent<Unit>(out Unit unit))
        {
            unit.Stats.ToggleIsAboveDropPoint(false);
        }
    }
}