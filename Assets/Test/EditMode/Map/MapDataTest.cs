using NUnit.Framework;

public class MapDataTest
{
    [Test]
    public void Constructor_CorrectlyInitializeTheObject()
    {
        MapData mapData = new MapData(5, 1.0f);
        Assert.AreEqual(5, mapData.LaneCount);
        Assert.AreEqual(1.0f, mapData.LaneSize);
        Assert.IsFalse(mapData.IsPersistance);
    }
}