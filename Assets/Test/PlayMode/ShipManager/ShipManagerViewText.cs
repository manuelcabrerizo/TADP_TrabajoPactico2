using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ShipManagerViewText
{
    [UnityTest]
    public IEnumerator Spawn_CorrectlySpawnShipViews()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        ShipManagerView view = gameObject.AddComponent<ShipManagerView>();

        // Act
        Vector2 spawnPosition = new Vector2(1.0f, 2.0f);
        ShipView shipViewRight = view.Spawn(spawnPosition, LaneDirection.Right) as ShipView;
        ShipView shipViewLeft = view.Spawn(spawnPosition, LaneDirection.Left) as ShipView;

        // Assert
        Assert.IsNotNull(shipViewRight && shipViewLeft);
        Assert.AreEqual(spawnPosition, shipViewRight.Position);
        Assert.AreEqual(spawnPosition, shipViewLeft.Position);
        SpriteRenderer spriteRendererRight = shipViewRight.GetComponent<SpriteRenderer>();
        Assert.IsFalse(spriteRendererRight.flipY);
        Assert.IsNotNull(spriteRendererRight.sprite);
        SpriteRenderer spriteRendererLeft = shipViewLeft.GetComponent<SpriteRenderer>();
        Assert.IsTrue(spriteRendererLeft.flipY);
        Assert.IsNotNull(spriteRendererLeft.sprite);
        yield return null;
    }

}
