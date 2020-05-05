using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
    public abstract class GattCharacteristic
    {
        public GattCharacteristic(string name, GattID uuid, SigAttributeProperties properties)
        {
        }
    }

    public class TimeCharacteristic : GattCharacteristic
    {
        public TimeCharacteristic(string name, GattID uuid, SigAttributeProperties properties) : base(name, uuid, properties)
        {
        }
    }

    public class TextCharacteristic : GattCharacteristic
    {
        public TextCharacteristic(string name, GattID uuid, SigAttributeProperties properties) : base(name, uuid, properties)
        {
        }
    }

}