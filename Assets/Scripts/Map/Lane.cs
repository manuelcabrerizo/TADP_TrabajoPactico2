using System;

[Serializable]
public struct Lane
{
    public LaneType Type;
    public LaneDirection Direction; 
    public float TimeToSpawn;
    public float Speed;
    public int Index;
}
