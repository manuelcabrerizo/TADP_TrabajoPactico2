using System.Collections.Generic;

public interface IShipManagerModel
{
    public Lane Lane { get; set; }
    public List<ShipPresenter> SpawnedShips { get; set; }
    public void AddShip(ShipPresenter ship);
    public void RemoveShip(ShipPresenter ship);
}
