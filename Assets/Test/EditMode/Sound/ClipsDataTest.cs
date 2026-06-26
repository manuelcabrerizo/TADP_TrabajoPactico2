using NUnit.Framework;
using UnityEngine;

public class ClipsDataTest
{
    [Test]
    public void Constructor_CorrectlyInitializeTheObject()
    {
        ClipsData clipsData = new ClipsData(new AudioClip[] { });
        Assert.IsNotNull(clipsData.Clips);
        Assert.IsFalse(clipsData.IsPersistance);
    }
}

