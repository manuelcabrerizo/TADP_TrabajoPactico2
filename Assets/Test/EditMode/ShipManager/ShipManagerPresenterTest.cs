using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ShipManagerPresenterTest
{
    private const float WORLD_SIZE = 100.0f;
    private const int LANE_COUNT = 7;
    private const float LANE_SIZE = 1.0f;
    private const float TIME_TO_SPAWN = 1.0f;

    IShipManagerModelMock model = null;
    IShipManagerViewMock view = null;
    ShipManagerPresenter presenter = null;
    MethodInfo onUpdateMethod = null;

    [OneTimeSetUp]
    public void SetupOnce()
    {
        ServiceProvider.Instance.ClearAllServices();
        ServiceProvider.Instance.AddService<MapData>(new MapData(LANE_COUNT, LANE_SIZE));
    }

    [SetUp]
    public void Setup()
    {
        Lane lane = new Lane();
        lane.Type = LaneType.Street;
        lane.Direction = LaneDirection.Left;
        lane.TimeToSpawn = TIME_TO_SPAWN;
        lane.Speed = 5.0f;
        lane.Index = 1;
        model = new IShipManagerModelMock(lane);
        view = new IShipManagerViewMock();
        presenter = new ShipManagerPresenter(model, view, WORLD_SIZE);
        GetUpdateMethod();
    }

    [Test]
    public void Constructor_CorrectlyBindTheCallbacks()
    {
        Assert.AreEqual(1, view.OnUpdate.GetInvocationList().Length);
        Assert.AreEqual(1, view.OnTerminate.GetInvocationList().Length);
    }

    [Test]
    public void OnTerminateCallback_CorrectlyUnbindTheCallbacks()
    {
        view.OnTerminate.Invoke();
        Assert.IsNull(view.OnUpdate);
        Assert.IsNull(view.OnTerminate);
    }

    [Test]
    public void OnTerminateCallback_ClearPendingTasks()
    {
        view.OnTerminate.Invoke();
        onUpdateMethod.Invoke(presenter, new object[] { model.Lane.TimeToSpawn * 2.0f });
        Assert.AreEqual(0, model.AddShipTimesCall);
        Assert.AreEqual(0, view.SpawnTimesCall);
    }

    [Test]
    public void OnTerminateCallback_ClearSpawnedShips()
    {
        model.AddShip(new ShipPresenter(new IShipModelMock(), view.Spawn(Vector2.zero, model.Lane.Direction)));
        model.AddShip(new ShipPresenter(new IShipModelMock(), view.Spawn(Vector2.zero, model.Lane.Direction)));
        view.OnTerminate.Invoke();
        Assert.Zero(model.SpawnedShips.Count);
    }

    [TestCase(TIME_TO_SPAWN*0.5f, 0)]
    [TestCase(TIME_TO_SPAWN,      1)]
    [TestCase(TIME_TO_SPAWN*1.5f, 1)]
    [TestCase(TIME_TO_SPAWN*2.0f, 2)]
    [TestCase(TIME_TO_SPAWN*10.0f, 10)]
    public void OnUpdateCallback_SpawnNewShipAtTheCorrectTime(float timeToWait, int expected)
    {
        while (timeToWait >= model.Lane.TimeToSpawn)
        {
            view.OnUpdate.Invoke(model.Lane.TimeToSpawn);
            timeToWait -= model.Lane.TimeToSpawn;
        }
        Assert.AreEqual(expected, model.AddShipTimesCall);
        Assert.AreEqual(expected, view.SpawnTimesCall);
        Assert.AreEqual(expected, model.SpawnedShips.Count);
    }

    [Test]
    public void OnUpdateCallback_CorrectlyRemoveOutOfBoundShips()
    {
        model.AddShip(new ShipPresenter(new ShipModel(model.Lane.Speed, Vector2.right), view.Spawn(new Vector2(0.0f, 0.0f), model.Lane.Direction)));
        model.AddShip(new ShipPresenter(new ShipModel(model.Lane.Speed, Vector2.right), view.Spawn(new Vector2(100.0f, 0.0f), model.Lane.Direction)));
        model.AddShip(new ShipPresenter(new ShipModel(model.Lane.Speed, Vector2.right), view.Spawn(new Vector2(-100.0f, 0.0f), model.Lane.Direction)));
        view.OnUpdate(0.016f);
        Assert.AreEqual(1, model.SpawnedShips.Count);
    }

    private void GetUpdateMethod()
    {
        onUpdateMethod = presenter.GetType().GetMethod(presenter.OnUpdateMethodName, BindingFlags.NonPublic | BindingFlags.Instance);
    }
}

public class IShipManagerModelMock : IShipManagerModel
{
    public int AddShipTimesCall = 0;
    public int RemoveShipTimesCall = 0;

    public Lane Lane { get; set; }
    public List<ShipPresenter> SpawnedShips { get; set; }

    public IShipManagerModelMock(Lane lane)
    {
        Lane = lane;
        SpawnedShips = new List<ShipPresenter>();
    }

    public void AddShip(ShipPresenter ship)
    {
        AddShipTimesCall++;
        SpawnedShips.Add(ship);
    }
    public void RemoveShip(ShipPresenter ship)
    {
        RemoveShipTimesCall++;
        SpawnedShips.Remove(ship);
    }
}

public class IShipManagerViewMock : IShipManagerView
{
    public int SpawnTimesCall = 0;

    public Action OnTerminate { get; set; }
    public Action<float> OnUpdate { get; set; }
    public IShipView Spawn(Vector2 spawnPosition, LaneDirection direction)
    {
        SpawnTimesCall++;
        IShipView view = new IShipViewMock();
        view.Position = spawnPosition;
        return view;
    }
}