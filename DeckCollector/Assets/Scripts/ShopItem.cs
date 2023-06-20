using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "NewShopItem")]
public class ShopItem : ScriptableObject
{
    public Unit unit;
    public int id;
    public string shopItemName;
}
