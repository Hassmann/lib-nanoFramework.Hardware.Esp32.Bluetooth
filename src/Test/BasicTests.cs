using Microsoft.VisualStudio.TestTools.UnitTesting;
using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;
using System.Diagnostics;

namespace Test
{
    [TestClass]
    public class BasicTests
    {
        private const string deviceName = "Test Device";

        private const int MaxStringLength = 10;

        private SimulatedDevice device;
        private BluetoothHost host;
        private GattService service;

        #region Initialization

        [TestInitialize]
        public void BeforeEach()
        {
            device = SimulatedDevice.Reset();

            host = BluetoothHost.Initialize(new BluetoothHostConfiguration(deviceName));
            service = CreateTestService();
            host.AddService(service);
        }

        [TestCleanup]
        public void AfterEach()
        {
        }

        private GattService CreateTestService()
        {
            return new GattService("E6B4E8D6-00F2-44EE-B78D-4CF4330922BE",
                new TimeCharacteristic("Time", SigCharacteristic.CurrentTime, SigAttributeProperties.Read),
                new TextCharacteristic("Text", "EAD840EE-4F73-4AC7-ACCA-13F8229D08D7", SigAttributeProperties.Read | SigAttributeProperties.WriteWithoutResponse, MaxStringLength)
                );
        }

        #endregion Initialization

        [TestMethod]
        public void Host_Initialized()
        {
            Assert.AreEqual(deviceName, device.DeviceName);
        }

        [TestMethod]
        public void Service_Initialized()
        {
            var timeCharacteristic = service["Time"];
            Assert.IsNotNull(timeCharacteristic);
            Assert.AreSame(timeCharacteristic, service[0]);

            var textCharacteristic = service["Text"];
            Assert.IsNotNull(textCharacteristic);
            Assert.AreSame(textCharacteristic, service[1]);
        }

        [TestMethod]
        public void Service_Started()
        {
            host.Advertise();

            
        }


        [TestMethod]
        public void Service_Notify()
        {
            var textCharacteristic = service["Text"] as TextCharacteristic;

            bool gotNotified = false;

            textCharacteristic.ValueChanged += (uuid) =>
            {
                gotNotified = true;
            };

            host.Advertise();

            BluetoothHost.Target.TestSetString(service, textCharacteristic, "Test");

            Assert.IsTrue(gotNotified);
            Assert.AreEqual("Test", textCharacteristic.Value);
        }

    }
}