using System;
using System.Collections;
using UnityEngine;

public interface IPlayerView
{
    public Action OnStart { get; set; }
    public Action OnTerminate { get; set; }
    public Action MoveUp { get; set; }
    public Action MoveDown { get; set; }
    public Action Collision { get; set; }
    public Vector2 Position { get; set; }
    void PlayClip(AudioClip clip);
    public object PlayCoroutine(IEnumerator coroutine);
    public void EndCoroutine(object coroutine);
}
