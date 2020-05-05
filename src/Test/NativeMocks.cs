using System.Diagnostics;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    internal class SimulatedDevice
    {
        public static SimulatedDevice Current { get; private set; }

        public static SimulatedDevice Reset() => Current = new SimulatedDevice();

        public string DeviceName { get; private set; }
        public BluetoothMode Mode { get; private set; }
        public int MaxTransferUnit { get; private set; }

        internal void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
        {
            DeviceName = deviceName;
            Mode = mode;
            MaxTransferUnit = maxTransferUnit;

            Trace.WriteLine(@"/* Initialize NVS. */
    nvs_flash_init();

    esp_bt_controller_mem_release(ESP_BT_MODE_CLASSIC_BT);

    esp_bt_controller_config_t bt_cfg = BT_CONTROLLER_INIT_CONFIG_DEFAULT();
    esp_bt_controller_init(&bt_cfg);
    esp_bt_controller_enable(ESP_BT_MODE_BLE);

    esp_bluedroid_init();

    esp_bluedroid_enable();

    esp_ble_gatts_register_callback(gatts_event_handler);

    esp_ble_gap_register_callback(gap_event_handler);
    esp_err_t local_mtu_ret = esp_ble_gatt_set_local_mtu(500);

    // Store DeviceName
");
        }


        #region Table



        #endregion
    }

    partial class BluetoothHost
    {
        private static SimulatedDevice Target => SimulatedDevice.Current;

        private static void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
            => Target.NativeInitializeDevice(deviceName, mode, maxTransferUnit);
    }
}