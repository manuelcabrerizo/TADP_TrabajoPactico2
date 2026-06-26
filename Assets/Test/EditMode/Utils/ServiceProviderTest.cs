using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

public class ServiceProviderTest
{
    ConstructorInfo serviceProviderConstructor = null;
    ServiceProvider serviceProvider = null;

    [OneTimeSetUp]
    public void SetupOnce()
    {
        GetConstructor();
    }

    [SetUp]
    public void Setup()
    {
        serviceProvider = serviceProviderConstructor.Invoke(new object[] { }) as ServiceProvider;
    }

    [Test]
    public void ServiceProviderInstance_CorrectlyReturnsSameObjectEveryTime()
    {
        var instance1 = ServiceProvider.Instance;
        var instance2 = ServiceProvider.Instance;
        Assert.AreEqual(instance1, instance2);
    }

    [Test]
    public void ServiceProviderInstance_CorrectlyReturnsTheInstance()
    {
        // Assert
        Assert.IsNotNull(ServiceProvider.Instance);
    }

    [Test]
    public void AddService_CorrectlyGetsAdded()
    {
        // Arrange
        IService service = new PersistantService();
        // Act
        serviceProvider.AddService<PersistantService>(service);
        // Assert
        Assert.AreEqual(service, serviceProvider.GetService<PersistantService>());
    }

    [Test]
    public void AddRepeatedService_CorrectlyGetsIgnored()
    {
        // Arrange
        IService service0 = new PersistantService();
        IService service1 = new PersistantService();
        // Act
        serviceProvider.AddService<PersistantService>(service0);
        serviceProvider.AddService<PersistantService>(service1);
        // Assert
        Assert.AreEqual(service0, serviceProvider.GetService<PersistantService>());
        Assert.AreEqual(1, serviceProvider.Count);
    }

    [Test]
    public void RemoveService_CorrectlyGetsRemoved()
    {
        // Arrange
        serviceProvider.AddService<PersistantService>(new PersistantService());
        serviceProvider.AddService<NoPersistantService>(new NoPersistantService());
        // Act
        serviceProvider.RemoveService<PersistantService>();
        // Assert
        Assert.Throws<KeyNotFoundException>(() => serviceProvider.GetService<PersistantService>());
        Assert.DoesNotThrow(() => serviceProvider.GetService<NoPersistantService>());
        Assert.AreEqual(1, serviceProvider.Count);
    }

    [Test]
    public void RemoveService_ThrowsWhenTheServiceIsNotFound()
    {
        Assert.Throws<KeyNotFoundException>(() => serviceProvider.RemoveService<PersistantService>());
        Assert.Throws<KeyNotFoundException>(() => serviceProvider.RemoveService<NoPersistantService>());
    }

    [Test]
    public void ConstainsService_CorrectlyDetectExistingServices()
    {
        // Arrange
        serviceProvider.AddService<NoPersistantService>(new NoPersistantService());
        // Assert
        Assert.IsTrue(serviceProvider.ContainsService<NoPersistantService>());
        Assert.IsFalse(serviceProvider.ContainsService<PersistantService>());
    }

    [Test]
    public void GetService_CorreclyReturnTheAskedService()
    {
        // Arrange
        IService service0 = new PersistantService();
        IService service1 = new NoPersistantService();
        // Act
        serviceProvider.AddService<PersistantService>(service0);
        serviceProvider.AddService<NoPersistantService>(service1);
        // Assert
        Assert.AreEqual(service0, serviceProvider.GetService<PersistantService>());
        Assert.AreEqual(service1, serviceProvider.GetService<NoPersistantService>());
    }

    [Test]
    public void GetService_ThrowsWhenTheServiceIsNotFound()
    {
        Assert.Throws<KeyNotFoundException>(() => serviceProvider.GetService<PersistantService>());
        Assert.Throws<KeyNotFoundException>(() => serviceProvider.GetService<NoPersistantService>());
    }

    [Test]
    public void ClearAllServices_CorrectlyRemovesAllServices()
    {
        // Arrange
        serviceProvider.AddService<PersistantService>(new PersistantService());
        serviceProvider.AddService<NoPersistantService>(new NoPersistantService());
        // Act
        serviceProvider.ClearAllServices();
        // Assert
        Assert.Throws<KeyNotFoundException>(() => serviceProvider.GetService<PersistantService>());
        Assert.Throws<KeyNotFoundException>(() => serviceProvider.GetService<NoPersistantService>());
        Assert.AreEqual(0, serviceProvider.Count);
    }

    [Test]
    public void ClearAllNonPersitanceServices_CorrectlyRemovesAllNonPeristanceServices()
    {
        // Arrange
        IService service0 = new PersistantService();
        IService service1 = new NoPersistantService();
        // Act
        serviceProvider.AddService<PersistantService>(service0);
        serviceProvider.AddService<NoPersistantService>(service1);
        serviceProvider.ClearAllNonPersitanceServices();
        // Assert
        Assert.AreEqual(service0, serviceProvider.GetService<PersistantService>());
        Assert.Throws<KeyNotFoundException>(() => serviceProvider.GetService<NoPersistantService>());
        Assert.AreEqual(1, serviceProvider.Count);
    }

    private void GetConstructor()
    {
        var serviceProviderConstructors = typeof(ServiceProvider).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
        if(serviceProviderConstructors.Length > 0)
        {
            serviceProviderConstructor = serviceProviderConstructors[0];
        }
    }

    public class NoPersistantService : IService
    {
        public bool IsPersistance => false;
    }

    public class PersistantService : IService
    {
        public bool IsPersistance => true;
    }
}