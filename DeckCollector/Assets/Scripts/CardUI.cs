using UnityEngine;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Unit unitPrefab;
    private int cardValue = 2;
    public bool isAboveSellPoint;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isAboveSellPoint)
        {
            Debug.Log("SOLD!");
            GameManager.Instance.GoldCount += cardValue;
            GameManager.Instance.deck.SellCard(unitPrefab);
            Destroy(gameObject);
        }
        else
        {
            PlayCard();
        }
    }

    private void PlayCard()
    {
        GameManager.Instance.deck.PlayCard(unitPrefab, transform);
        Destroy(gameObject);
    }
}