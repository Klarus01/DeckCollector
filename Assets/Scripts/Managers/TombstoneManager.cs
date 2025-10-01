using System;
using System.Collections.Generic;
using UnityEngine;

public class TombstoneManager : SingletonMonobehaviour<TombstoneManager>
{
    [SerializeField] private GameObject tombstonePrefab;
    public List<Tombstone> tombstones = new();

    public void RegisterDeath(Unit unit, Vector3 position)
    {
        unit.OnDeath += () => CreateTombstone(position, unit);
    }

    public void AddTombstone(Tombstone tombstone)
    {
        if (!tombstones.Contains(tombstone))
        {
            tombstones.Add(tombstone);
        }
    }

    public void RemoveTombstone(Tombstone tombstone)
    {
        if (tombstones.Contains(tombstone))
        {
            tombstones.Remove(tombstone);
        }
    }

    private void CreateTombstone(Vector3 position, Unit unit)
    {
        var tombstoneInstance = Instantiate(tombstonePrefab, position, Quaternion.identity);
        tombstoneInstance.GetComponent<Tombstone>().Initialize(unit);
    }
}