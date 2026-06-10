public interface IPlayerModel
{
    public int CurrentLane { get; set; }
    public bool IsMoving { get; set; }
    public bool IsFinish { get; set; }
    public bool CanNotMove { get; }
}
