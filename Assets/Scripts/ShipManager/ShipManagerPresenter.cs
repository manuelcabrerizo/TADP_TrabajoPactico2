using System.Collections.Generic;
using UnityEngine;

public class ShipManagerPresenter
{
    MapData MapData => ServiceProvider.Instance.GetService<MapData>();

    private ShipManagerModel model = null;
    private ShipManagerView view = null;
    private TaskScheduler taskScheduler = null;

    private float worldSize = 0;

    public ShipManagerPresenter(ShipManagerModel model, ShipManagerView view)
    {
        this.model = model;
        this.view = view;
        taskScheduler = new TaskScheduler();
        taskScheduler.Schedule(OnSpawnShip, model.Lane.TimeToSpawn);
        worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x * 2;
        view.OnTerminate += OnTerminate;
        view.OnUpdate += OnUpdate;
    }

    private void OnTerminate()
    {
        view.OnTerminate -= OnTerminate;
        view.OnUpdate -= OnUpdate;
        taskScheduler.Clear();
        model.SpawnedShips.Clear();
    }

    private void OnSpawnShip()
    {
        Vector2 spawnPosition = new Vector2();
        Vector2 direction = model.Lane.Direction == LaneDirection.Left ? Vector2.right : Vector2.left;
        spawnPosition.x = -direction.x * worldSize * 0.5f;
        spawnPosition.y = model.Lane.Index * MapData.LaneSize;
        model.AddShip(new ShipPresenter(new ShipModel(model.Lane.Speed, direction), view.Spawn(spawnPosition, model.Lane.Direction)));
        taskScheduler.Schedule(OnSpawnShip, model.Lane.TimeToSpawn);
    }

    public void OnUpdate(float deltaTime)
    { 
        taskScheduler.Tick(deltaTime);
        List<ShipPresenter> toRemove = new List<ShipPresenter>();
        foreach (ShipPresenter shipPresenter in model.SpawnedShips)
        {
            if (shipPresenter.Position.x < -worldSize * 0.5f || shipPresenter.Position.x > worldSize * 0.5f)
            {
                toRemove.Add(shipPresenter);
            }
        }
        foreach (ShipPresenter shipPresenter in toRemove)
        {

            shipPresenter.Terminate();
            model.RemoveShip(shipPresenter);
            
        }
        toRemove.Clear();
    }
}