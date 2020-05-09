using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
    public delegate void ToSignalCharacteristicChange(GattID uuid);

    public abstract class GattCharacteristic
    {
        public GattCharacteristic(string name, GattID uuid, SigAttributeProperties properties)
        {
            Name = name;
            UUID = uuid;
            Properties = properties;
        }

        public event ToSignalCharacteristicChange ValueChanged;

        public string Name { get; private set; }
        public GattID UUID { get; private set; }
        public SigAttributeProperties Properties { get; private set; }

        protected void SignalChange()
            => ValueChanged?.Invoke(UUID);

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

        private string value;

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

    }

}