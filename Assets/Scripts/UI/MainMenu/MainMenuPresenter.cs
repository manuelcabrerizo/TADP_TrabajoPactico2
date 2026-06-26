public class MainMenuPresenter
{
    private IMainMenuModel model = null;
    private IMainMenuView view = null;

    public MainMenuPresenter(IMainMenuModel model, IMainMenuView view)
    {
        this.model = model;
        this.view = view;

        view.OnTerminate += OnTerminate;
        view.OnPlay += OnPlay;
    }

    private void OnTerminate()
    {
        view.OnTerminate -= OnTerminate;
        view.OnPlay -= OnPlay;
    }

    private void OnPlay()
    {
        view.LoadScene("Gameplay");
    }
}
