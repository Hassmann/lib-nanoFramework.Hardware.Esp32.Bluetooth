using System.Diagnostics;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    using Test;

    partial class BluetoothHost
    {
        internal static SimulatedDevice Target => SimulatedDevice.Current;

        private static void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
            => Target.NativeInitializeDevice(deviceName, mode, maxTransferUnit);

        private static void NativePrepareGatt(int numEntries, int totalBytes)
            => Target.NativePrepareGatt(numEntries, totalBytes);

        private static void NativeAddEntry(int index, byte[] uuid, bool autoRespond, int maxLength, int permissions, byte[] value)
            => Target.NativeAddEntry(index, uuid, autoRespond, maxLength, permissions, value);

    }
}