using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nanoFramework.Hardware.Esp32.Bluetooth;

namespace Test
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void Service_Initialized()
        {
            var service = BTLEService.Start();
        }
    }
}
