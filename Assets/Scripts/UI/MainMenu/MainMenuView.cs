using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IMainMenuView
{
    [SerializeField] private Button playButton;

    public Action OnTerminate {  set; get; }
    public Action OnPlay {  set; get; }

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClick);
        OnTerminate?.Invoke();
    }

    private void OnPlayButtonClick()
    {
        OnPlay?.Invoke();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}