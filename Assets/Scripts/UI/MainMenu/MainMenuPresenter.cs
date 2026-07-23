public class MainMenuPresenter
{
    private IMainMenuModel model = null;
    private IMainMenuView view = null;

    public MainMenuPresenter(IMainMenuModel model, IMainMenuView view)
    {
        this.model = model;
        this.view = view;

        view.OnStart += OnStart;
        view.OnTerminate += OnTerminate;
        view.OnPlay += OnPlay;
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
        view.OnPlay -= OnPlay;
    }

    private void OnPlay()
    {
        view.PlayButtonSound();
        view.LoadScene("Gameplay");
    }
}
