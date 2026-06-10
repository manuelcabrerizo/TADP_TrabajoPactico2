using System;
using UnityEngine;

public interface IShipView
{
    public Action OnTerminate { get; set; }
    public Action<float> OnUpdate { get; set; }
    public Vector2 Position { get; set; }
    public void Delete();
}
