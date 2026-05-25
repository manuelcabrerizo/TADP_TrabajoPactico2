public class MapData : IService
{
    public bool IsPersistance => false;
    public int LaneCount { get; private set; } = 0;
    public float LaneSize { get; private set; } = 0.0f;
    public MapData(int laneCount, float laneSize)
    { 
        LaneCount = laneCount;
        LaneSize = laneSize;
    }
}
