using System;

public interface IMainMenuView
{
    public Action OnStart { get; set; }
    public Action OnTerminate {  get; set; }
    public Action OnPlay { get; set; }
    public void LoadScene(string sceneName);
    public void PlayMusic();
    public void StopMusic();
    public void PlayButtonSound();
}
