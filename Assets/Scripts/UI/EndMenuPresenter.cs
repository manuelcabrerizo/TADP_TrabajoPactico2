using UnityEngine.SceneManagement;

public class EndMenuPresenter
{
    private EndMenuModel model = null;
    private EndMenuView view = null;

    public EndMenuPresenter(EndMenuModel model, EndMenuView view)
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
        SceneManager.LoadScene("Gameplay");
    }
}
