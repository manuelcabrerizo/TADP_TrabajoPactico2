using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuView : MonoBehaviour, IEndMenuView
{
    private AudioManager AudioManager => ServiceProvider.Instance.GetService<AudioManager>();
    [SerializeField] private Button playAgainButton;

    public Action OnStart { get; set; }
    public Action OnTerminate { get; set; }
    public Action OnPlayAgain { get; set; }

    private void Awake()
    {
        playAgainButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void Start()
    {
        OnStart?.Invoke();
    }

    private void OnDestroy()
    {
        OnTerminate?.Invoke();
    }

    private void OnPlayButtonClick()
    {
        AudioManager.PlayClip(AudioManager.Sounds.ButtonClick);
        OnPlayAgain?.Invoke();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PlayMusic()
    {
        AudioManager.PlayMusic(AudioManager.Sounds.EndGameMusic);
    }

    public void StopMusic()
    {
        AudioManager.StopMusic();
    }

    public void PlayButtonSound()
    {
        playAgainButton.onClick.RemoveListener(OnPlayButtonClick);
    }
}
