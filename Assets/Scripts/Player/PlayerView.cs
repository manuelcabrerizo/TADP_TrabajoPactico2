using System;
using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    private AudioManager AudioManager => ServiceProvider.Instance.GetService<AudioManager>();
    public Action OnStart { get; set; }
    public Action OnTerminate { get; set; }
    public Action MoveUp { get; set; }
    public Action MoveDown { get; set; }
    public Action Collision { get; set; }

    public Vector2 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void Start()
    {
        OnStart?.Invoke();
    }

    private void OnDestroy()
    {
        OnTerminate?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collision?.Invoke();
    }
    
    public void PlayMusic()
    {
        AudioManager.PlayMusic(AudioManager.Sounds.GameplayMusic);
    }

    public void StopMusic()
    {
        AudioManager.StopMusic();
    }

    public void PlayMoveSound()
    {
        AudioManager.PlayClip(AudioManager.Sounds.ShipSfx);
    }

    public void PlayCrashSound()
    {
        AudioManager.PlayClip(AudioManager.Sounds.CrashSfx);
    }

    public object PlayCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public void EndCoroutine(object coroutine)
    {
        StopCoroutine(coroutine as Coroutine);
    }
}