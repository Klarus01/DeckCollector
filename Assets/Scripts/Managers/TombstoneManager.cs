using System.Collections.Generic;

public class TombstoneManager : SingletonMonobehaviour<TombstoneManager>
{
    public List<Tombstone> tombstones = new();

    public void AddTombstone(Tombstone tombstone)
    {
        tombstones.Add(tombstone);
    }

    public void RemoveTombstone(Tombstone tombstone)
    {
        tombstones.Remove(tombstone);
    }
}