public class Tombstone : Building
{
    public Unit originalUnit;
    
    public void Initialize(Unit unit)
    {
        originalUnit = unit;
        GameManager.Instance.deck.cardsAsTombstone.Add(this);
    }

    private void OnMouseDown()
    {
        originalUnit.ReviveUnit();
        GameManager.Instance.deck.cardsAsTombstone.Remove(this);
        Destroy(gameObject);
    }
}
