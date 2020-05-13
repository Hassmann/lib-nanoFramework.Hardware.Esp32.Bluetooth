using System.Runtime.CompilerServices;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{

    partial class BluetoothHost
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativePrepareGatt(int[] characteristicCount, int numEntries, int totalBytes, int maxValueSize);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeAddEntry(int index, byte[] uuid, bool autoRespond, int maxLength, int permissions, byte[] value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern byte[] NativeGetValue(int serviceIndex, int characteristicIndex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeBeginService(int serviceIndex, int entryIndex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeBeginCharacteristic(int serviceIndex, int characteristicIndex, int entryIndex, int entryCount);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeAdvertise(byte[] data, int mode, int filter);

    }
}