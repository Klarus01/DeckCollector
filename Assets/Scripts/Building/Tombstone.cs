public class Tombstone : Building
{
    public Unit originalUnit;
    
    public void Initialize(Unit unit)
    {
        originalUnit = unit;
    }

    private void OnMouseDown()
    {
        originalUnit.ReviveUnit();
    }
}
