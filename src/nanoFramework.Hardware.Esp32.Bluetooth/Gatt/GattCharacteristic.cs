using System;
using System.Text;

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

        public abstract int MaxDataSize { get; }
        public abstract byte[] Data { get; }

        public int Index { get; set; }

        public virtual int EntryCount => 2;

        public virtual OS.GattEntry[] Entries => new OS.GattEntry[]
        {
            new OS.GattEntry
            {
                AutoRespond = true,
                UUID = UUID,
                MaxLength = UUID.Bytes.Length,
                Permissions = Properties,
                Value = Data,
            },
        };

        protected void SignalChange()
            => ValueChanged?.Invoke(UUID);

        internal abstract void UpdateValue(byte[] value);
    }

    public class TimeCharacteristic : GattCharacteristic
    {
        public TimeCharacteristic(string name, GattID uuid, SigAttributeProperties properties) : base(name, uuid, properties)
        {
        }

        public override int MaxDataSize => OS.Size.Time;

        private DateTime value;

        public DateTime Value
        {
            get => value;
            set { this.value = value; }
        }


        public override byte[] Data
            => BitConverter.GetBytes(value.Ticks);

        internal override void UpdateValue(byte[] value)
        {
            var ticks = BitConverter.ToInt64(value, 0);

            this.value = new DateTime(ticks);
        }
    }

    public class TextCharacteristic : GattCharacteristic
    {
        private string value;

        public TextCharacteristic(string name, GattID uuid, SigAttributeProperties properties, int maxLength) : base(name, uuid, properties)
        {
            MaxLength = maxLength;
        }

        public string Value
        {
            get => value;

            set { this.value = value; }
        }

        public int MaxLength { get; private set; }

        public override int MaxDataSize
            => MaxLength * OS.Size.Character;
        
        public override byte[] Data
            => OS.Encode(value);

        internal override void UpdateValue(byte[] value)
        {
            this.value = OS.Decode(value);
        }
    }
}