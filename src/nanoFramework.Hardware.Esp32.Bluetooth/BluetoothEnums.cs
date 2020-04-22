namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    public enum BluetoothMode
    {
        Idle, //ESP_BT_MODE_IDLE = 0x00,   /*!< Bluetooth is not running */
        LowEnergy, //ESP_BT_MODE_BLE = 0x01,   /*!< Run BLE mode */
        Classic, //ESP_BT_MODE_CLASSIC_BT = 0x02,   /*!< Run Classic BT mode */
        Dual //ESP_BT_MODE_BTDM = 0x03,   /*!< Run dual mode */
    }
}