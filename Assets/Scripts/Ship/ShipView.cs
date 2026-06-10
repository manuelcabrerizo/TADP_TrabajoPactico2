using System;
using UnityEngine;

public class ShipView : MonoBehaviour, IShipView
{
    public Action OnTerminate { get; set; }
    public Action<float> OnUpdate { get; set; }
    public Vector2 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void OnDestroy()
    {
        OnTerminate?.Invoke();
    }

    private void Update()
    {
        OnUpdate?.Invoke(Time.deltaTime);
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
