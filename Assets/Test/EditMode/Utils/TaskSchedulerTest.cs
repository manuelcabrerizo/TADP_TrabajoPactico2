using NUnit.Framework;

public class TaskSchedulerTest
{
    TaskScheduler taskScheduler = null;
    TestObject testObject = null;

    [SetUp]
    public void Setup()
    {
        taskScheduler = new TaskScheduler();
        testObject = new TestObject();
    }

    [TestCase(1.0f, 0.5f, 0)]
    [TestCase(3.0f, 12.0f, 1)]
    [TestCase(0.9f, 0.999f, 1)]
    [TestCase(0.999f, 0.9f, 0)]
    [TestCase(0.5f, 0.5f, 1)]
    [TestCase(0.0f, 0.0f, 1)]
    [TestCase(-1.0f, 1.0f, 0)]
    public void Schedule_InvokeTaskAfterTimeToWait(float timeToWait, float timePass, int expectedTimesCalled)
    {
        // Arrange
        taskScheduler.Schedule(testObject.MakeAction, timeToWait);
        // Act
        taskScheduler.Tick(timePass);
        // Assert
        Assert.AreEqual(expectedTimesCalled, testObject.TimesCalled);
    }

    [TestCase(new float[] {0.2f, 1.0f, 5.0f}, 0.1f, 0)]
    [TestCase(new float[] { 0.2f, 1.0f, 5.0f }, 0.5f, 1)]
    [TestCase(new float[] { 0.2f, 1.0f, 5.0f }, 1.5f, 2)]
    [TestCase(new float[] { 0.2f, 1.0f, 5.0f }, 5.5f, 3)]
    public void Tick_InvokeTheCorrectAmountOfTaskGivenPassTime(float[] timeToWaits, float timePass, int expectedTimesCalled)
    {
        // Arrange
        foreach(float timeToWait in timeToWaits)
        {
            taskScheduler.Schedule(testObject.MakeAction, timeToWait);
        }
        // Act
        taskScheduler.Tick(timePass);
        // Assert
        Assert.AreEqual(expectedTimesCalled, testObject.TimesCalled);
    }

    [Test]
    public void Clear_RemoveAllTask()
    {
        // Arrange
        taskScheduler.Schedule(testObject.MakeAction, 1.0f);
        taskScheduler.Schedule(testObject.MakeAction, 1.5f);
        taskScheduler.Schedule(testObject.MakeAction, 2.0f);
        // Act
        taskScheduler.Clear();
        taskScheduler.Tick(3.0f);
        // Assert
        Assert.AreEqual(0, testObject.TimesCalled);
    }

    public class TestObject
    {
        public int TimesCalled { get; private set; }
        public void MakeAction()
        {
            TimesCalled++;
        }
    }
}


