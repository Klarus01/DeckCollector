using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Unit unitPrefab;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PlayCard();
    }

    private void PlayCard()
    {
        Unit newUnit = Instantiate(unitPrefab, transform.position, Quaternion.identity);
        GameManager.Instance.unitsOnBoard.Add(newUnit);
        Destroy(gameObject);
    }
}
