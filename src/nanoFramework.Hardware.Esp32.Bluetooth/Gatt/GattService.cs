using System;
using System.Collections;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
    public partial class GattService
    {
        public GattID UUID { get; private set; }

        public GattCharacteristic[] Characteristics { get; private set; }

        public int Index { get; set; }

        public GattService(GattID uuid, params GattCharacteristic[] characteristics)
        {
            UUID = uuid;
            Characteristics = characteristics ?? new GattCharacteristic[0];

            for (int i = 0; i < Characteristics.Length; i++)
            {
                Characteristics[i].Index = i;
            }
        }

        public GattCharacteristic this[string name]
        {
            get
            {
                foreach (var characteristic in Characteristics)
                {
                    if (characteristic.Name == name)
                    {
                        return characteristic;
                    }
                }

                throw new IndexOutOfRangeException();
            }
        }

        public GattCharacteristic this[int index] 
            => Characteristics[index];

        public int EntryCount
        {
            get
            {
                int count = 1; // Service

                foreach (GattCharacteristic characteristic in Characteristics)
                {
                    count += characteristic.EntryCount;
                }

                return count;
            }
        }

        public int MaxDataSize
        { 
            get
            {
                int maxSize = 0;

                foreach (GattCharacteristic characteristic in Characteristics)
                {
                    maxSize += characteristic.MaxDataSize;
                }

                return maxSize;
            }
        }

        public bool IsPrimary { get; set; } = true;

        internal OS.GattEntry[] ServiceEntries
            => new OS.GattEntry[]
            { 
                new OS.GattEntry
                {
                    AutoRespond = true,
                    UUID = IsPrimary ? SigAttributeType.PrimaryService : SigAttributeType.SecondaryService,
                    Permissions = SigAttributeProperties.Read,
                    MaxLength = UUID.Bytes.Length, //
                    Value = UUID.Bytes,
                }
            };

    }
}