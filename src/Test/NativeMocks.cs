using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    partial class BluetoothHost
    {
        private static void NativeCheckInterop(string text)
        {
            Trace.WriteLine(@"/* Initialize NVS. */


    ??? esp_ble_gatts_app_register(ESP_APP_ID);

");
        }


        private static void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
        {
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



        private static void TraceNative([CallerMemberName]string method = "")
        {
            Trace.WriteLine($"Native call: {method}");
        }
    }
}