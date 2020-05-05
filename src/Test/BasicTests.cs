using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;

namespace Test
{

    [TestClass]
    public class BasicTests
    {
        SimulatedDevice device;
        const string deviceName = "Test Device";

        BluetoothHost host;
        GattService service;

        [TestInitialize]
        public void BeforeEach()
        {
            device = SimulatedDevice.Reset();

            host = BluetoothHost.Initialize(new BluetoothHostConfiguration(deviceName));
            service = CreateTestService();
        }

        private GattService CreateTestService()
        {
            return new GattService("E6B4E8D6-00F2-44EE-B78D-4CF4330922BE",
                new TimeCharacteristic("Time", SigCharacteristic.CurrentTime, SigAttributeProperties.Read),
                new TextCharacteristic("Text", "EAD840EE-4F73-4AC7-ACCA-13F8229D08D7", SigAttributeProperties.Read | SigAttributeProperties.WriteWithoutResponse)
                );
        }

        [TestCleanup]
        public void AfterEach()
        {
        }

        [TestMethod]
        public void Service_Initialized()
        {
        }

        [TestMethod]
        public void Service_Started()
        {
            host.AddService(service);
            host.Advertise();
        }


    }
}
