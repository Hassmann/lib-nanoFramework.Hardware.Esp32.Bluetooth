using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    public partial class BTLEService
    {
        // Browse our samples repository: https://github.com/nanoframework/samples
        // Check our documentation online: https://docs.nanoframework.net/
        // Join our lively Discord community: https://discord.gg/gCyBu8T

        private BTLEService(BTLEServiceSettings settings)
        {
            NativeCheckInterop("Hello ESP32");
        }


        public static BTLEService Start(BTLEServiceSettings settings = null)
        {
            settings = settings ?? new BTLEServiceSettings();

            return new BTLEService(settings);
        }

    }
}
