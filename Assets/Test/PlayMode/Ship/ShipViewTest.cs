using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ShipViewTest
{
    [UnityTest]
    public IEnumerator Update_CorrectlyMoveTheShip()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        ShipView view = gameObject.AddComponent<ShipView>();
        ShipModel model = new ShipModel(10.0f, Vector2.right);
        ShipPresenter presenter = new ShipPresenter(model, view);
        view.Position = Vector3.zero;

        // Act
        yield return new WaitForSeconds(2.0f);

        // Assert
        Vector2 Destination = new Vector2(20.0f, 0.0f);
        Assert.GreaterOrEqual(view.Position.x, 19.5f);
        Assert.LessOrEqual(view.Position.x, 20.5f);
        Assert.AreEqual(Destination.y, view.Position.y);
    }
}
