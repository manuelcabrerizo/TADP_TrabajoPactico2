using NUnit.Framework;
using System;
using UnityEngine;

public class ShipPresenterTest
{
    IShipModelMock model = null;
    IShipViewMock view = null;
    ShipPresenter presenter = null;

    [SetUp]
    public void Setup()
    {
        model = new IShipModelMock();
        view = new IShipViewMock();
        presenter = new ShipPresenter(model, view);
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
    public void OnUpdateCallback_CorrectlyUpdatePosition()
    {
        float timeToUpdate = 5.0f;
        Vector2 startPosition = view.Position;
        view.OnUpdate(timeToUpdate);
        Vector2 expectedPosition = startPosition + model.Direction * (model.Speed * timeToUpdate);
        Assert.AreEqual(expectedPosition, view.Position);
    }


    [Test]
    public void Position_ReturnsThePositionFromTheView()
    {
        Vector2 currentPosition = presenter.Position;
        Assert.AreEqual(1, view.PositionTimesCall);
        Assert.AreEqual(view.Position, currentPosition);
    }

    [Test]
    public void Terminate_CallViewDeleteOneTime()
    {
        presenter.Terminate();
        Assert.AreEqual(1, view.DeleteTimesCall);
    }
}

public class IShipModelMock : IShipModel
{
    public float Speed { get; private set; } = 10.0f;
    public Vector2 Direction { get; private set; } = Vector2.right;
}

public class IShipViewMock : IShipView
{
    public int PositionTimesCall = 0;
    public int DeleteTimesCall = 0;

    public Action OnTerminate { get; set; }
    public Action<float> OnUpdate { get; set; }
    public Vector2 Position
    {
        get 
        {
            PositionTimesCall++;
            return position;
        }
        set
        {
            position = value;
        }
    }
        
    private Vector2 position = Vector2.zero;

    public void Delete()
    {
        DeleteTimesCall++;
    }
}