using System;

public interface IEndMenuView
{
    public Action OnStart { get; set; }
    public Action OnTerminate { get; set; }
    public Action OnPlayAgain { get; set; }
    public void LoadScene(string sceneName);
    public void PlayMusic();
    public void StopMusic();
    public void PlayButtonSound();
}
