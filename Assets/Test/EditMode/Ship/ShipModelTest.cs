using NUnit.Framework;
using UnityEngine;

public class ShipModelTest
{
    [Test]
    public void Constructor_CorrectlyInitializeValues()
    {
        ShipModel model = new ShipModel(100.0f, Vector2.up);
        Assert.AreEqual(100.0f, model.Speed);
        Assert.AreEqual(Vector2.up, model.Direction);
    }
}

