using UnityEngine;

public class EndMenuInitializer : MonoBehaviour
{
    [SerializeField] private EndMenuView endMenuView;

    private void Awake()
    {
        new EndMenuPresenter(new EndMenuModel(), endMenuView);
    }
}
