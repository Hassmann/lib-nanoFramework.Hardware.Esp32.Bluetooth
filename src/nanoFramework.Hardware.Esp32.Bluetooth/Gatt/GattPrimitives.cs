using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
    public struct GattID
    {
        public GattID(string uuid)
        {
            var parsed = new Guid(uuid);
            Bytes = parsed.ToByteArray();
        }

        public GattID(ushort value)
        {
            Bytes = BitConverter.GetBytes(value);
        }

        public GattID(byte[] bytes)
        {
            Bytes = bytes;
        }

        public byte[] Bytes { get; private set; }

        public static implicit operator GattID(string uuid)
            => new GattID(uuid);

        public static implicit operator GattID(byte[] uuid)
            => new GattID(uuid);

        public static implicit operator GattID(SigService service)
            => new GattID((ushort)service);

        public static implicit operator GattID(SigCharacteristic characteristic)
            => new GattID((ushort)characteristic);

        public static implicit operator GattID(SigAttributeType attributeType)
            => new GattID((ushort)attributeType);
    }
}