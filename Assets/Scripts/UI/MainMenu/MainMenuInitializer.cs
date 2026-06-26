using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    [SerializeField] private MainMenuView mainMenuView;

    private void Awake()
    {
        new MainMenuPresenter(new MainMenuModel(), mainMenuView);
    }
}
