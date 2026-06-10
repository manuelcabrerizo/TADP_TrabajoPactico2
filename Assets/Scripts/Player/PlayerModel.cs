
public class PlayerModel : IPlayerModel
{
    public int CurrentLane { get; set; } = 0;
    public bool IsMoving { get; set; } = false;
    public bool IsFinish { get; set; } = true;
    public bool CanNotMove => IsMoving || IsFinish;
}
