using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipManagerView : MonoBehaviour
{
    [SerializeField] private ShipView shipPrefab;
    [SerializeField] private Sprite[] shipSprites;

    public Action OnTerminate;
    public Action<float> OnUpdate;

    private void OnDestroy()
    {
        OnTerminate?.Invoke();
    }

    private void Update()
    {
        OnUpdate?.Invoke(Time.deltaTime);
    }

    public ShipView Spawn(Vector2 spawnPosition, LaneDirection direction)
    {
        ShipView shipView = Instantiate(shipPrefab, transform);
        SpriteRenderer spriteRenderer = shipView.GetComponent<SpriteRenderer>();

        if (direction == LaneDirection.Left)
            spriteRenderer.flipY = true;
        spriteRenderer.sprite = shipSprites[Random.Range(0, shipSprites.Length)];
        shipView.transform.position = spawnPosition;
        
        return shipView;
    }
}