using System.Diagnostics;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    using Test;

    partial class BluetoothHost
    {
        internal static SimulatedDevice Target => SimulatedDevice.Current;

        private static void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
            => Target.NativeInitializeDevice(deviceName, mode, maxTransferUnit);


    }
}