using NUnit.Framework;

public class ShipManagerModelTest
{
    Lane lane;
    ShipManagerModel model = null;

    [SetUp]
    public void Setup()
    {
        lane = new Lane();
        lane.Type = LaneType.Street;
        lane.Direction = LaneDirection.Left;
        lane.TimeToSpawn = 2.0f;
        lane.Speed = 5.0f;
        lane.Index = 1;
        model = new ShipManagerModel(lane);
    }

    [Test]
    public void Constructor_CorrectlyInitializeValues()
    {

        Assert.AreEqual(lane, model.Lane);
        Assert.IsNotNull(model.SpawnedShips);
    }

    [Test]
    public void AddShip_CorrectlyAddAShip()
    {
        int currentShipCount = model.SpawnedShips.Count;
        ShipPresenter ship = new ShipPresenter(new IShipModelMock(), new IShipViewMock());
        model.AddShip(ship);
        Assert.AreEqual(currentShipCount + 1, model.SpawnedShips.Count);
    }

    [Test]
    public void RemoveShip_CorrectlyRemovesTheShip()
    {
        int currentShipCount = model.SpawnedShips.Count;
        ShipPresenter ship = new ShipPresenter(new IShipModelMock(), new IShipViewMock());
        model.AddShip(ship);

        model.RemoveShip(ship);

        Assert.AreEqual(currentShipCount, model.SpawnedShips.Count);
    }
}