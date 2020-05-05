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

        public byte[] Bytes { get; private set; }

        public static implicit operator GattID(string uuid)
            => new GattID(uuid);

        public static implicit operator GattID(SigService service)
            => new GattID((ushort)service);

        public static implicit operator GattID(SigCharacteristic characteristic)
            => new GattID((ushort)characteristic);
    }
}