using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuView : MonoBehaviour, IEndMenuView
{
    [SerializeField] private Button playAgainButton;

    public Action OnTerminate { get; set; }
    public Action OnPlayAgain { get; set; }

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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
