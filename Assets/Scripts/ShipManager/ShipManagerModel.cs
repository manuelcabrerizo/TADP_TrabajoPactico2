using System.Collections.Generic;

public class ShipManagerModel : IShipManagerModel
{
    public Lane Lane { get; set; }
    public List<ShipPresenter> SpawnedShips { get; set; } = null;

    public ShipManagerModel(Lane lane)
    {
        Lane = lane;
        SpawnedShips = new List<ShipPresenter>();
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
