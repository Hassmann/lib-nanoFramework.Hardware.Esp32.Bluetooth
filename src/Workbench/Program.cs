using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;
using System;
using System.Threading;

namespace Workbench
{

    public class Program
    {
        private const string deviceName = "Test Device";

        private static BluetoothHost host;
        private static GattService service;

        public static void Main()
        {
            host = BluetoothHost.Initialize(new BluetoothHostConfiguration(deviceName));

            //service = new GattService("E6B4E8D6-00F2-44EE-B78D-4CF4330922BE")
            //    .AddCharacteristic(
            //        "Time", typeof(DateTime),
            //        SigCharacteristic.CurrentTime,
            //        SigAttributeProperties.Read)
            //    .AddCharacteristic(
            //        "Text", typeof(string),
            //        "EAD840EE-4F73-4AC7-ACCA-13F8229D08D7",
            //        SigAttributeProperties.Read | SigAttributeProperties.WriteWithoutResponse);


            //host.AddService(service);
            //host.Advertise();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}