using UnityEngine;

public class PlayerModel
{
    public int CurrentLane { get; set; } = 0;
    public bool IsMoving { get; set; } = false;
    public bool IsFinish { get; set; } = true;
    public Coroutine AnimationCoroutine { get; set; } = null;
    public bool CanNotMove => IsMoving || IsFinish;
}
