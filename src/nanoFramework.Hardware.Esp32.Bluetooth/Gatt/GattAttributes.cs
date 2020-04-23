using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
    public class GattServiceAttribute : Attribute
    {
        public GattServiceAttribute(string uuid)
        {
        }
    }

    public class GattCharacteristicAttribute : Attribute
    {
        public GattCharacteristicAttribute(SigCharacteristic characteristic)
        {
        }

        public GattCharacteristicAttribute(string uuid)
        {
        }
    }
}