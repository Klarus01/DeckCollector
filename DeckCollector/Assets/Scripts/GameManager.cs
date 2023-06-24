using System;
using System.Collections.Generic;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    public int goldCount = 0;
    public int partsCount = 0;
    public List<Unit> unitsOnBoard = new();
    public List<Unit> deck = new();
    public List<Unit> units = new();
    public event Action OnUIUpdate;

    public void UpdateUI()
    {
        OnUIUpdate?.Invoke();
    }
}
