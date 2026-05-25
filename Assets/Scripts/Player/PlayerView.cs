using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerView : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public Action OnStart;
    public Action OnTerminate;
    public Action MoveUp;
    public Action MoveDown;
    public Action Collision;

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
}