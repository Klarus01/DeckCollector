using UnityEngine;

public class Tombstone : Building
{
    public Unit originalUnit;
    
    public void Initialize(Unit unit)
    {
        originalUnit = unit;
        TombstoneManager.Instance.AddTombstone(this);
        GameManager.Instance.deck.cardsAsTombstone.Add(this);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    private void OnMouseDown()
    {
        originalUnit.ReviveUnit();
        TombstoneManager.Instance.RemoveTombstone(this);
        GameManager.Instance.deck.cardsAsTombstone.Remove(this);
        Destroy(gameObject);
    }
}