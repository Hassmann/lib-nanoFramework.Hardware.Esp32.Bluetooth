namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    public enum BluetoothMode
    {
        Idle, //ESP_BT_MODE_IDLE = 0x00,   /*!< Bluetooth is not running */
        LowEnergy, //ESP_BT_MODE_BLE = 0x01,   /*!< Run BLE mode */
        Classic, //ESP_BT_MODE_CLASSIC_BT = 0x02,   /*!< Run Classic BT mode */
        Dual //ESP_BT_MODE_BTDM = 0x03,   /*!< Run dual mode */
    }

    /// Advertising mode
    public enum AdvertisingMode{
        General = 0x00,
        DirectedHigh = 0x01,
        Scannable = 0x02,
        NonConnectable = 0x03,
        DirectedLow = 0x04,
    }


    public enum AdvertisingFilter
    {
        ///Allow both scan and connection requests from anyone
        AnyScanAnyConnection,
        ///Allow both scan req from White List devices only and connection req from anyone
        WhitelistScanAnyConnection,
        ///Allow both scan req from anyone and connection req from White List devices only
        AnyScanWhitelistConnection,
        ///Allow scan and connection requests from White List devices only
        WhitelistScanWhitelistConnection,
    }
}