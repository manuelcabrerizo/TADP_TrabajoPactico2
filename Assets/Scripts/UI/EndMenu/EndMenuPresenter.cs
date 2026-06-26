public class EndMenuPresenter
{
    private IEndMenuModel model = null;
    private IEndMenuView view = null;

    public EndMenuPresenter(IEndMenuModel model, IEndMenuView view)
    {
        this.model = model;
        this.view = view;

        view.OnTerminate += OnTerminate;
        view.OnPlayAgain += OnPlayAgain;
    }

    private void OnTerminate()
    {
        view.OnTerminate -= OnTerminate;
        view.OnPlayAgain -= OnPlayAgain;
    }

    private void OnPlayAgain()
    {
        view.LoadScene("Gameplay");
    }
}
