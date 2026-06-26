using System;

public interface IEndMenuView
{
    public Action OnTerminate { get; set; }
    public Action OnPlayAgain { get; set; }
    public void LoadScene(string sceneName);
}
