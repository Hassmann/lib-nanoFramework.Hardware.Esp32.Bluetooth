using System.Runtime.CompilerServices;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    partial class BTLEService
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void NativeCheckInterop(string text);
    }
}