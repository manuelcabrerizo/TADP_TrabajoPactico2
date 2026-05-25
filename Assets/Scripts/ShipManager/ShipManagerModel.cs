using System.Collections.Generic;
using UnityEngine;

public class ShipManagerModel
{
    public Lane Lane { get; private set; }
    public List<ShipPresenter> SpawnedShips { get; private set; } = new List<ShipPresenter>();

    public ShipManagerModel(Lane lane)
    {
        Lane = lane;
    }

    public void AddShip(ShipPresenter ship)
    {
        SpawnedShips.Add(ship);
    }

    public void RemoveShip(ShipPresenter ship)
    {
        SpawnedShips.Remove(ship);
    }
}
