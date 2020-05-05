using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
    public partial class GattService
    {
        public GattID UUID { get; private set; }

        public GattService(GattID uuid, params GattCharacteristic[] characteristics)
        {
            UUID = uuid;
        }

        public GattService AddCharacteristic(string name, Type type, SigCharacteristic sigCharacteristic, SigAttributeProperties properties)
        {

            return this;
        }

        public GattService AddCharacteristic(string name, Type type, string uuid, SigAttributeProperties properties)
        {

            return this;
        }
    }
}