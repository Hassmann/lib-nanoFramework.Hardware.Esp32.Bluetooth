using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;

namespace Test
{
    [GattProfile()]
    class TestService : GattService
    {

    }


    [TestClass]
    public class BasicTests
    {
        [TestInitialize]
        public void BeforeEach()
        {
            BluetoothHost.Initialize(BluetoothMode.LowEnergy);
        }

        [TestCleanup]
        public void AfterEach()
        {

        }

        [TestMethod]
        public void Service_Initialized()
        {
            //var service = BluetoothHost.AddService();
        }

        [TestMethod]
        public void Service_Started()
        {
        }


    }
}
