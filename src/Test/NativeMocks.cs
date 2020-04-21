using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    partial class BTLEService
    {
        private static void NativeCheckInterop(string text)
        {
            TraceNative();
        }

        private static void TraceNative([CallerMemberName]string method = "")
        {
            Trace.WriteLine($"Native call: {method}");
        }
    }
}