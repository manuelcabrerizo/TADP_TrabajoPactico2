using System;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuView : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;

    public Action OnTerminate;
    public Action OnPlayAgain;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnDestroy()
    {
        playAgainButton.onClick.RemoveListener(OnPlayButtonClick);
        OnTerminate?.Invoke();
    }

    private void OnPlayButtonClick()
    {
        OnPlayAgain?.Invoke();
    }
}
