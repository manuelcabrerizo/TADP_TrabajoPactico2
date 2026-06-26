using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShipManagerView : MonoBehaviour, IShipManagerView
{
    private ShipView shipPrefab;
    private Sprite[] shipSprites;

    public Action OnTerminate { get; set; }
    public Action<float> OnUpdate { get; set; }

    private void Awake()
    {
        shipPrefab = Resources.Load<ShipView>("Prefabs/Ship");
        shipSprites = Resources.LoadAll<Sprite>("Sprites");
    }

    private void OnDestroy()
    {
        OnTerminate?.Invoke();
    }

    private void Update()
    {
        OnUpdate?.Invoke(Time.deltaTime);
    }

    public IShipView Spawn(Vector2 spawnPosition, LaneDirection direction)
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