using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPresenter
{
    private MapData MapData => ServiceProvider.Instance.GetService<MapData>();
    private ClipsData ClipsData => ServiceProvider.Instance.GetService<ClipsData>();

    private IPlayerModel model = null;
    private IPlayerView view = null;
    public object AnimationCoroutine { get; set; } = null;

    public PlayerPresenter(IPlayerModel model, IPlayerView view)
    {
        this.model = model;
        this.view = view;

        view.OnStart += OnStart;
        view.OnTerminate += OnTerminate;
        view.MoveUp += OnMoveUp;
        view.MoveDown += OnMoveDown;
        view.Collision += OnCollision;
    }

    private void OnStart()
    {
        view.PlayCoroutine(StartAnimation());
    }

    private void OnTerminate()
    {
        view.OnStart -= OnStart;
        view.OnTerminate -= OnTerminate;
        view.MoveUp -= OnMoveUp;
        view.MoveDown -= OnMoveDown;
        view.Collision -= OnCollision;
    }

    private void OnMoveUp()
    {
        if (model.CanNotMove)
            return;
        view.PlayClip(ClipsData.Clips[0]);
        model.IsMoving = true;
        model.CurrentLane = Math.Min(model.CurrentLane + 1, MapData.LaneCount - 1);
        UpdatePosition(Vector2.up * (model.CurrentLane * MapData.LaneSize));
    }

    private void OnMoveDown()
    {
        if (model.CanNotMove)
            return;
        view.PlayClip(ClipsData.Clips[0]);
        model.IsMoving = true;
        model.CurrentLane = Math.Max(model.CurrentLane - 1, 0);
        UpdatePosition(Vector2.up * (model.CurrentLane * MapData.LaneSize));
    }

    private void OnCollision()
    {
        view.PlayClip(ClipsData.Clips[1]);
        if (AnimationCoroutine != null)
        {
            view.EndCoroutine(AnimationCoroutine);
            AnimationCoroutine = null;
            MoveFinish();
        }
        model.CurrentLane = 0;
        view.Position = Vector2.up * (model.CurrentLane * MapData.LaneSize);
    }

    private void MoveFinish()
    { 
        model.IsMoving = false;
        if (model.CurrentLane == MapData.LaneCount - 1)
        {
            model.IsFinish = true;
            view.PlayCoroutine(FinishAnimation());
        }
    }

    private void UpdatePosition(Vector2 targetPosition)
    {
        if (AnimationCoroutine != null)
        {
            view.EndCoroutine(AnimationCoroutine);
        }
        AnimationCoroutine = view.PlayCoroutine(MoveAnimation(targetPosition));
    }

    private IEnumerator StartAnimation()
    {
        Time.timeScale = 10.0f;
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1.0f;
        model.IsFinish = false;
    }

    private IEnumerator FinishAnimation()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("EndMenu");
    }

    private IEnumerator MoveAnimation(Vector2 target)
    {
        Vector2 start = view.Position;
        float speed = 8.0f;
        float t = 0.0f;
        while (t <= 1.0f)
        {
            view.Position = Vector2.Lerp(start, target, t);
            t += speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        view.Position = target;
        MoveFinish();
    }
}
