using UnityEngine;

public class ShipModel
{
    public float Speed { get; private set; } = 0;
    public Vector2 Direction { get; private set; } = Vector2.zero;

    public ShipModel(float speed, Vector2 direction)
    {
        Speed = speed;
        Direction = direction;
    }
}
