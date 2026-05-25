using System;
using UnityEngine;

public class ShipView : MonoBehaviour
{
    public Action OnTerminate;
    public Action<float> OnUpdate;

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
