using System;

public interface IMainMenuView
{
    public Action OnTerminate {  get; set; }
    public Action OnPlay { get; set; }
    public void LoadScene(string sceneName);
}
