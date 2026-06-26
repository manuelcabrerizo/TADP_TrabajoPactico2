using NUnit.Framework;
using System;

public class MainMenuPresenterTest
{
    private IMainMenuModelMock model = null;
    private IMainMenuViewMock view = null;
    private MainMenuPresenter presenter = null;

    [SetUp]
    public void Setup()
    { 
        model = new IMainMenuModelMock();
        view = new IMainMenuViewMock();
        presenter = new MainMenuPresenter(model, view);
    }

    [Test]
    public void Constructor_CorrectlyBindTheCallbacks()
    {
        Assert.AreEqual(1, view.OnTerminate.GetInvocationList().Length);
        Assert.AreEqual(1, view.OnPlay.GetInvocationList().Length);
    }

    [Test]
    public void OnTerminateCallback_CorrectlyUnbindTheCallbacks()
    {
        view.OnTerminate.Invoke();
        Assert.IsNull(view.OnTerminate);
        Assert.IsNull(view.OnPlay);
    }

    [Test]
    public void OnPlayCallback_CorrectlyGoToGameplayScene()
    {
        view.OnPlay();
        Assert.AreEqual(1, view.LoadSceneTimesCall);
        Assert.AreEqual("Gameplay", view.LoadSceneSceneName);
    }
}

public class IMainMenuModelMock : IMainMenuModel
{ 
}

public class IMainMenuViewMock : IMainMenuView
{
    public int LoadSceneTimesCall = 0;
    public string LoadSceneSceneName = string.Empty;

    public Action OnTerminate { get; set; }
    public Action OnPlay { get; set; }

    public void LoadScene(string sceneName)
    {
        LoadSceneTimesCall++;
        LoadSceneSceneName = sceneName;
    }
}