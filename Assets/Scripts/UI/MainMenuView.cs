using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button playButton;

    public Action OnTerminate;
    public Action OnPlay;


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
}