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
        Assert.IsNull(view.OnPlayAgain);
    }

    [Test]
    public void OnPlayAgainCallback_CorrectlyCallsPlayButtonSound()
    {
        view.OnPlayAgain.Invoke();
        Assert.AreEqual(1, view.PlayButtonSoundTimesCall);
    }

    [Test]
    public void OnPlayAgainCallback_CorrectlyGoToGameplayScene()
    {
        view.OnPlayAgain.Invoke();
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

    public int PlayMusicTimesCall = 0;
    public int StopMusicTImesCall = 0;
    public int PlayButtonSoundTimesCall = 0;

    public Action OnStart { get; set; }
    public Action OnTerminate { get; set; }
    public Action OnPlayAgain { get; set; }

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