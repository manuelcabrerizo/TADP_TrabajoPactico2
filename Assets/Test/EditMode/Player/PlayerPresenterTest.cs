using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;

public class PlayerPresenterTest
{
    private MapData MapData => ServiceProvider.Instance.GetService<MapData>();

    private const int LANE_COUNT = 7;
    private const float LANE_SIZE = 1.0f;

    private IPlayerModelMock model = null;
    private IPlayerViewMock view = null;
    private PlayerPresenter presenter = null;

    [OneTimeSetUp]
    public void SetupOnce()
    {
        ServiceProvider.Instance.ClearAllServices();
        ServiceProvider.Instance.AddService<MapData>(new MapData(LANE_COUNT, LANE_SIZE));
        ServiceProvider.Instance.AddService<ClipsData>(new ClipsData(new AudioClip[] { null, null }));
    }

    [SetUp]
    public void Setup()
    { 
        model = new IPlayerModelMock();
        view = new IPlayerViewMock();
        presenter = new PlayerPresenter(model, view);
    }

    [Test]
    public void Constructor_CorrectlyBindTheCallbacks()
    {
        Assert.AreEqual(1, view.OnStart.GetInvocationList().Length);
        Assert.AreEqual(1, view.OnTerminate.GetInvocationList().Length);
        Assert.AreEqual(1, view.MoveUp.GetInvocationList().Length);
        Assert.AreEqual(1, view.MoveDown.GetInvocationList().Length);
        Assert.AreEqual(1, view.Collision.GetInvocationList().Length);
    }

    [Test]
    public void OnStartCallback_CallsPlayCoroutine()
    {
        view.OnStart.Invoke();
        Assert.AreEqual(1, view.PlayCoroutineTimesCall);
    }

    [Test]
    public void OnTerminateCallback_CorrectlyUnbindTheCallbacks()
    {
        view.OnTerminate.Invoke();
        Assert.IsNull(view.OnStart);
        Assert.IsNull(view.OnTerminate);
        Assert.IsNull(view.MoveUp);
        Assert.IsNull(view.MoveDown);
        Assert.IsNull(view.Collision);
    }

    [TestCase(false, false, 1)]
    [TestCase(true, false, 0)]
    [TestCase(false, true, 0)]
    [TestCase(true, true, 0)]
    public void MoveUpCallback_CallsPlayClip(bool isMoving, bool isFinish, int expected)
    {
        model.IsMoving = isMoving;
        model.IsFinish = isFinish;
        view.MoveUp.Invoke();
        Assert.AreEqual(expected, view.PlayClipTimesCall);
    }

    [TestCase(false, false, true)]
    [TestCase(true, false, true)]
    [TestCase(false, true, false)]
    [TestCase(true, true, true)]
    public void MoveUpCallback_SetIsMoving(bool isMoving, bool isFinish, bool expected)
    {
        model.IsMoving = isMoving;
        model.IsFinish = isFinish;
        view.MoveUp.Invoke();
        Assert.AreEqual(expected, model.IsMoving);
    }

    [TestCase(0, 1, 1)]
    [TestCase(0, 5, 5)]
    [TestCase(0, 6, LANE_COUNT - 1)]
    [TestCase(0, 7, LANE_COUNT - 1)]
    [TestCase(LANE_COUNT - 1, 1, LANE_COUNT - 1)]
    [TestCase(LANE_COUNT - 1, 2, LANE_COUNT - 1)]
    public void MoveUpCallback_IncrementCurrentLaneAndDontGoOverMaxLaneCount(int currentLane, int moveUpAmount, int expected)
    {
        model.CurrentLane = currentLane;
        for (int i = 0; i < moveUpAmount; i++)
        {
            model.IsMoving = false;
            model.IsFinish = false;
            view.MoveUp();
        }
        Assert.AreEqual(expected, model.CurrentLane);
    }

    [Test]
    public void MoveUpCallback_CorrectlyPlayAnimationCoroutineWhenIsNotNull()
    {
        model.IsMoving = false;
        model.IsFinish = false;
        presenter.AnimationCoroutine = new object();
        view.MoveUp();
        Assert.AreEqual(1, view.EndCoroutineTimesCall);
        Assert.AreEqual(1, view.PlayCoroutineTimesCall);
        Assert.IsNotNull(presenter.AnimationCoroutine);
    }

    [Test]
    public void MoveUpCallback_CorrectlyPlayAnimationCoroutineWhenIsNull()
    {
        model.IsMoving = false;
        model.IsFinish = false;
        presenter.AnimationCoroutine = null;
        view.MoveUp();
        Assert.AreEqual(0, view.EndCoroutineTimesCall);
        Assert.AreEqual(1, view.PlayCoroutineTimesCall);
        Assert.IsNotNull(presenter.AnimationCoroutine);
    }

    [TestCase(false, false, 1)]
    [TestCase(true, false, 0)]
    [TestCase(false, true, 0)]
    [TestCase(true, true, 0)]
    public void MoveDownCallback_CallsPlayClip(bool isMoving, bool isFinish, int expected)
    {
        model.IsMoving = isMoving;
        model.IsFinish = isFinish;
        view.MoveDown.Invoke();
        Assert.AreEqual(expected, view.PlayClipTimesCall);
    }

    [TestCase(false, false, true)]
    [TestCase(true, false, true)]
    [TestCase(false, true, false)]
    [TestCase(true, true, true)]
    public void MoveDownCallback_SetIsMoving(bool isMoving, bool isFinish, bool expected)
    {
        model.IsMoving = isMoving;
        model.IsFinish = isFinish;
        view.MoveDown.Invoke();
        Assert.AreEqual(expected, model.IsMoving);
    }

    [TestCase(1, 1, 0)]
    [TestCase(2, 1, 1)]
    [TestCase(2, 3, 0)]
    [TestCase(LANE_COUNT - 1, LANE_COUNT, 0)]
    public void MoveDownCallback_DecrementCurrentLaneAndDontGoUnderZero(int currentLane, int moveUpAmount, int expected)
    {
        model.CurrentLane = currentLane;
        for (int i = 0; i < moveUpAmount; i++)
        {
            model.IsMoving = false;
            model.IsFinish = false;
            view.MoveDown();
        }
        Assert.AreEqual(expected, model.CurrentLane);
    }

    [Test]
    public void MoveDownCallback_CorrectlyPlayAnimationCoroutineWhenIsNotNull()
    {
        model.IsMoving = false;
        model.IsFinish = false;
        presenter.AnimationCoroutine = new object();
        view.MoveDown();
        Assert.AreEqual(1, view.EndCoroutineTimesCall);
        Assert.AreEqual(1, view.PlayCoroutineTimesCall);
        Assert.IsNotNull(presenter.AnimationCoroutine);
    }

    [Test]
    public void MoveDownCallback_CorrectlyPlayAnimationCoroutineWhenIsNull()
    {
        model.IsMoving = false;
        model.IsFinish = false;
        presenter.AnimationCoroutine = null;
        view.MoveDown();
        Assert.AreEqual(0, view.EndCoroutineTimesCall);
        Assert.AreEqual(1, view.PlayCoroutineTimesCall);
        Assert.IsNotNull(presenter.AnimationCoroutine);
    }

    [Test]
    public void CollisionCallback_CorrectlyCallsPlayClip()
    {
        view.Collision.Invoke();
        Assert.AreEqual(1, view.PlayClipTimesCall);
    }

    [TestCase(0, false, 0)]
    [TestCase(2, false, 0)]
    [TestCase(LANE_COUNT - 1, true, 1)]
    public void CollisionCallback_CorrectlyEndAnimationCoroutineWhenIsNotNull(
        int currentLane, bool expectedIsFinish, int expectedPlayCoroutineTimesCall)
    {
        presenter.AnimationCoroutine = new object();
        model.IsFinish = false;
        model.CurrentLane = currentLane;
        view.Collision.Invoke();

        Assert.AreEqual(1, view.EndCoroutineTimesCall);
        Assert.IsNull(presenter.AnimationCoroutine);
        Assert.IsFalse(model.IsMoving);
        Assert.AreEqual(expectedIsFinish, model.IsFinish);
        Assert.AreEqual(expectedPlayCoroutineTimesCall, view.PlayCoroutineTimesCall);
    }

    [Test]
    public void CollisionCallback_CorrectlyEndAnimationCoroutineWhenIsNull()
    {
        presenter.AnimationCoroutine = null;
        view.Collision.Invoke();
        Assert.AreEqual(0, view.EndCoroutineTimesCall);
        Assert.AreEqual(0, view.PlayCoroutineTimesCall);
    }

    [Test]
    public void CollisionCallback_CorrectlyResetPlayerPosition()
    {
        model.CurrentLane = 2;
        view.Position = Vector2.up * (model.CurrentLane * MapData.LaneSize);
        view.Collision.Invoke();
        Assert.AreEqual(0, model.CurrentLane);
        Assert.AreEqual(Vector2.up * (model.CurrentLane * MapData.LaneSize), view.Position);
    }
}

public class IPlayerModelMock : IPlayerModel
{
    public int CurrentLane { get; set; } = 0;
    public bool IsMoving { get; set; } = false;
    public bool IsFinish { get; set; } = true;
    public bool CanNotMove => IsMoving || IsFinish;
}

public class IPlayerViewMock : IPlayerView
{
    private Vector3 position = Vector3.zero;
    public int EndCoroutineTimesCall = 0;
    public int PlayClipTimesCall = 0;
    public int PlayCoroutineTimesCall = 0;

    public Action OnStart { get; set; }
    public Action OnTerminate { get; set; }
    public Action MoveUp { get; set; }
    public Action MoveDown { get; set; }
    public Action Collision { get; set; }

    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
    public void EndCoroutine(object coroutine)
    {
        EndCoroutineTimesCall++;
    }

    public void PlayClip(AudioClip clip)
    {
        PlayClipTimesCall++;
    }

    public object PlayCoroutine(IEnumerator coroutine)
    {
        PlayCoroutineTimesCall++;
        return new object();
    }
}