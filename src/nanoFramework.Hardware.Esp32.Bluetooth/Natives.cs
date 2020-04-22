using System.Runtime.CompilerServices;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    partial class BluetoothHost
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeCheckInterop(string text);
    }
}