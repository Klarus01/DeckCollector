using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "New Card Data")]
public class CardData : ScriptableObject
{
    public UnitData unitData;
    public float unitHealth;
}
