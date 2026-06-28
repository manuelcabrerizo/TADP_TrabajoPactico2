using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerPresenterCoroutinesTest
{
    private const int LANE_COUNT = 7;
    private const float LANE_SIZE = 1.0f;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        ServiceProvider.Instance.ClearAllServices();
        ServiceProvider.Instance.AddService<MapData>(new MapData(LANE_COUNT, LANE_SIZE));
        ServiceProvider.Instance.AddService<ClipsData>(new ClipsData(new AudioClip[] { null, null }));
        yield return null;
    }

    [UnityTest]
    public IEnumerator StartAnimation_CorrectlySetValue()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        PlayerView view = gameObject.AddComponent<PlayerView>();
        PlayerModel model = new PlayerModel();
        PlayerPresenter playerPresenter = new PlayerPresenter(model, view);

        playerPresenter.StartAnimationSpeed = 10.0f;
        playerPresenter.StartAnimationDuration = 1.5f;

        // Act
        view.PlayCoroutine(playerPresenter.StartAnimation());
        yield return null;

        // Assert
        Assert.AreEqual(playerPresenter.StartAnimationSpeed, Time.timeScale);
        Assert.AreEqual(true, model.IsFinish);
        yield return new WaitForSecondsRealtime(playerPresenter.StartAnimationDuration + 0.1f);
        Assert.AreEqual(1.0f, Time.timeScale);
        Assert.AreEqual(false, model.IsFinish);
    }


    [UnityTest]
    public IEnumerator MoveAnimation_CorretlyUpdatePosition()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        PlayerView view = gameObject.AddComponent<PlayerView>();
        view.Position = Vector3.zero;
        PlayerPresenter playerPresenter = new PlayerPresenter(new PlayerModel(), view);
        playerPresenter.Speed = 10.0f;

        // Act
        Vector2 destination = new Vector3(10.0f, 0.0f);
        view.PlayCoroutine(playerPresenter.MoveAnimation(destination));
        yield return new WaitForSeconds((1.0f / playerPresenter.Speed) + 0.5f);

        // Assert
        Assert.AreEqual(destination, view.Position);
    }
}
