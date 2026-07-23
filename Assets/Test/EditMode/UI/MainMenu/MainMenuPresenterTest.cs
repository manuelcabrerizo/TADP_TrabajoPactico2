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
    public void OnStartCallback_CorrectlyCallsPlayMusic()
    {
        view.OnStart.Invoke();
        Assert.AreEqual(1, view.PlayMusicTimesCall);
    }

    [Test]
    public void OnTerminateCallback_CorrectlyCallsStopMusic()
    {
        view.OnTerminate.Invoke();
        Assert.AreEqual(1, view.StopMusicTImesCall);
    }

    [Test]
    public void OnTerminateCallback_CorrectlyUnbindTheCallbacks()
    {
        view.OnTerminate.Invoke();
        Assert.IsNull(view.OnTerminate);
        Assert.IsNull(view.OnPlay);
    }

    [Test]
    public void OnPlayCallback_CorrectlyCallsPlayButtonSound()
    {
        view.OnPlay.Invoke();
        Assert.AreEqual(1, view.PlayButtonSoundTimesCall);
    }

    [Test]
    public void OnPlayCallback_CorrectlyGoToGameplayScene()
    {
        view.OnPlay.Invoke();
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

    public int PlayMusicTimesCall = 0;
    public int StopMusicTImesCall = 0;
    public int PlayButtonSoundTimesCall = 0;

    public Action OnStart { get; set; }
    public Action OnTerminate { get; set; }
    public Action OnPlay { get; set; }

    public void LoadScene(string sceneName)
    {
        LoadSceneTimesCall++;
        LoadSceneSceneName = sceneName;
    }

    public void PlayMusic()
    {
        PlayMusicTimesCall++;
    }

    public void StopMusic()
    {
        StopMusicTImesCall++;
    }

    public void PlayButtonSound()
    {
        PlayButtonSoundTimesCall++;
    }
}