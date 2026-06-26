using System;
using UnityEngine;

public interface IShipManagerView
{ 
    public Action OnTerminate { get; set; }
    public Action<float> OnUpdate { get; set; }

    public IShipView Spawn(Vector2 spawnPosition, LaneDirection direction);
}
