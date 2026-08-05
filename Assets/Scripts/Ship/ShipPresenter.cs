using UnityEngine;

public class ShipPresenter
{
    private IShipModel model = null;
    private IShipView view = null;

    public Vector2 Position => view.Position;
    public ShipPresenter(IShipModel model, IShipView view)
    {
        this.model = model;
        this.view = view;

        view.OnTerminate += OnTerminate;
        view.OnUpdate += OnUpdate;
    }

    private void OnTerminate()
    {
        view.OnTerminate -= OnTerminate;
        view.OnUpdate -= OnUpdate;
    }

    private void OnUpdate(float deltaTime)
    {
        Vector2 position = view.Position;
        //position += model.Direction * (model.Speed * deltaTime);
        view.Position = position;
    }

    public void Terminate()
    {
        view.Delete();
    }
}