using NUnit.Framework;

public class PlayerModelTest
{
    [TestCase(true, true, true)]
    [TestCase(true, false, true)]
    [TestCase(false, true, true)]
    [TestCase(false, false, false)]
    public void CanNotMove_CorrectlyReturnTheResult(bool isMoving, bool isFinish, bool expected)
    { 
        PlayerModel model = new PlayerModel();
        model.IsMoving = isMoving;
        model.IsFinish = isFinish;
        Assert.AreEqual(expected, model.CanNotMove);
    }

}