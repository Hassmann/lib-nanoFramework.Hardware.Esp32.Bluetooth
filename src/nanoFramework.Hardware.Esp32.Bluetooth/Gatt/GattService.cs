﻿using System;
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

        public OS.GattEntry ServiceEntry
            => new OS.GattEntry
            {
                AutoRespond = true,
                UUID = IsPrimary ? SigAttributeType.PrimaryService : SigAttributeType.SecondaryService,
                Permissions = SigAttributeProperties.Read,
                MaxLength = 2, //
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