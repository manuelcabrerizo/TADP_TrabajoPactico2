using NUnit.Framework;
using System;

public class EndMenuPresenterTest
{
    private IEndMenuModelMock model = null;
    private IEndMenuViewMock view = null;
    private EndMenuPresenter presenter = null;

    [SetUp]
    public void Setup()
    {
        model = new IEndMenuModelMock();
        view = new IEndMenuViewMock();
        presenter = new EndMenuPresenter(model, view);
    }

    [Test]
    public void Constructor_CorrectlyBindTheCallbacks()
    {
        Assert.AreEqual(1, view.OnTerminate.GetInvocationList().Length);
        Assert.AreEqual(1, view.OnPlayAgain.GetInvocationList().Length);
    }

    [Test]
    public void OnTerminateCallback_CorrectlyUnbindTheCallbacks()
    {
        view.OnTerminate.Invoke();
        Assert.IsNull(view.OnTerminate);
        Assert.IsNull(view.OnPlayAgain);
    }

    [Test]
    public void OnPlayAgainCallback_CorrectlyGoToGameplayScene()
    {
        view.OnPlayAgain();
        Assert.AreEqual(1, view.LoadSceneTimesCall);
        Assert.AreEqual("Gameplay", view.LoadSceneSceneName);
    }
}

public class IEndMenuModelMock : IEndMenuModel
{
}

public class IEndMenuViewMock : IEndMenuView
{
    public int LoadSceneTimesCall = 0;
    public string LoadSceneSceneName = string.Empty;

    public Action OnTerminate { get; set; }
    public Action OnPlayAgain { get; set; }

    public void LoadScene(string sceneName)
    {
        LoadSceneTimesCall++;
        LoadSceneSceneName = sceneName;
    }
}