using System;
using System.Collections;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
    public partial class GattService
    {
        public GattID UUID { get; private set; }

        public GattCharacteristic[] Characteristics { get; private set; }

        public GattService(GattID uuid, params GattCharacteristic[] characteristics)
        {
            UUID = uuid;
            Characteristics = characteristics ?? new GattCharacteristic[0];
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

        public OS.GattEntry ServiceEntry
            => new OS.GattEntry
            {
                AutoRespond = true,
            };

        public OS.GattEntry[] Entries
        {
            get
            {
                var entries = new OS.GattEntry[EntryCount];

                int index = 0;

                entries[index++] = ServiceEntry;

                foreach (GattCharacteristic characteristic in Characteristics)
                {
                    foreach (OS.GattEntry entry in characteristic.Entries)
                    {
                        entries[index++] = entry;
                    }
                }

                return entries;
            }
        }
    }
}