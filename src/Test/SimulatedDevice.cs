using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;
using System;
using System.Diagnostics;

namespace Test
{
    internal partial class SimulatedDevice
    {
        public static SimulatedDevice Current { get; private set; }

        public static SimulatedDevice Reset() => Current = new SimulatedDevice();

        public string DeviceName { get; private set; }
        public BluetoothMode Mode { get; private set; }
        public int MaxTransferUnit { get; private set; }


        internal void TestSetString(GattCharacteristic textCharacteristic, string value)
        {
            throw new NotImplementedException();
        }


    }
}
