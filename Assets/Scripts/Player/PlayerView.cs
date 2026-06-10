using System;
using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] AudioSource audioSource;

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

    public void PlayClip(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
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