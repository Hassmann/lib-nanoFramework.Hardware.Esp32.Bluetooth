using System.Runtime.CompilerServices;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{

    partial class BluetoothHost
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativePrepareGatt(int numEntries, int totalBytes);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeAddEntry(int index, byte[] uuid, bool autoRespond, int maxLength, int permissions, byte[] value);

    }
}