using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;

namespace Test
{
    [GattService("E6B4E8D6-00F2-44EE-B78D-4CF4330922BE")]
    class TestService : GattService
    {
        [GattCharacteristic(SigCharacteristic.CurrentTime)]
        public DateTime Time { get; }

        [GattCharacteristic("EAD840EE-4F73-4AC7-ACCA-13F8229D08D7")]
        public string Text { get; set; }
    }


    [TestClass]
    public class BasicTests
    {
        const string deviceName = "Test Device";

        BluetoothHost host;
        TestService service;

        [TestInitialize]
        public void BeforeEach()
        {
            host = BluetoothHost.Initialize(new BluetoothHostConfiguration(deviceName));

            service = new TestService();
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
