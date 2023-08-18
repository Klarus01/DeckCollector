using System;
using System.Collections.Generic;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    private int shopItemCost = 2;
    public int ShopCost { get { return shopItemCost; } set { shopItemCost = value; OnUIUpdate?.Invoke(); } }
    private int goldCount = 5;
    public int GoldCount { get { return goldCount; } set { goldCount = value; OnUIUpdate?.Invoke(); } }
    public int partsCount = 6;
    public int maxDeckSize = 8;
    public List<Unit> units = new();
    public Deck deck;
    public event Action OnUIUpdate;
    public event Action OnHandUpdate;

    public void UpdateUI()
    {
        OnUIUpdate?.Invoke();
    }

    public void UpdateHand()
    {
        OnHandUpdate?.Invoke();
    }
}
