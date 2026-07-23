using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IMainMenuView
{
    private AudioManager AudioManager => ServiceProvider.Instance.GetService<AudioManager>();
    [SerializeField] private Button playButton;

    public Action OnStart { get; set; }
    public Action OnTerminate {  set; get; }
    public Action OnPlay {  set; get; }

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void Start()
    {
        OnStart?.Invoke();
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

    public void PlayMusic()
    {
        AudioManager.PlayMusic(AudioManager.Sounds.MenuMusic);
    }

    public void StopMusic()
    {
        AudioManager.StopMusic();
    }

    public void PlayButtonSound()
    {
        AudioManager.PlayClip(AudioManager.Sounds.ButtonClick);
    }
}