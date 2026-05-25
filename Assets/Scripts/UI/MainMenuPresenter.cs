using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPresenter
{
    private MainMenuModel model = null;
    private MainMenuView view = null;

    public MainMenuPresenter(MainMenuModel model, MainMenuView view)
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
        SceneManager.LoadScene("Gameplay");
    }
}
