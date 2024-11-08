using UnityEngine;

public class Tombstone : Building
{
    public Unit originalUnit;
    
    public void Initialize(Unit unit)
    {
        originalUnit = unit;
        GameManager.Instance.deck.cardsAsTombstone.Add(this);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    private void OnMouseDown()
    {
        originalUnit.ReviveUnit();
        GameManager.Instance.deck.cardsAsTombstone.Remove(this);
        Destroy(gameObject);
    }
}
