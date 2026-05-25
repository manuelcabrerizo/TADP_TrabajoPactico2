using UnityEngine;

public class ShipPresenter
{
    private ShipModel model = null;
    private ShipView view = null;

    public Vector2 Position => view.Position;
    public ShipPresenter(ShipModel model, ShipView view)
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
        position += model.Direction * (model.Speed * Time.deltaTime);
        view.Position = position;
    }

    public void Terminate()
    {
        view.Delete();
    }
}