public class EndMenuPresenter
{
    private IEndMenuModel model = null;
    private IEndMenuView view = null;

    public EndMenuPresenter(IEndMenuModel model, IEndMenuView view)
    {
        this.model = model;
        this.view = view;
        view.OnStart += OnStart;
        view.OnTerminate += OnTerminate;
        view.OnPlayAgain += OnPlayAgain;
    }

    private void OnStart()
    {
        view.PlayMusic();
    }

    private void OnTerminate()
    {
        view.StopMusic();
        view.OnStart -= OnStart;
        view.OnTerminate -= OnTerminate;
        view.OnPlayAgain -= OnPlayAgain;
    }

    private void OnPlayAgain()
    {
        view.PlayButtonSound();
        view.LoadScene("Gameplay");
    }
}
