using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;

namespace Test
{
    using ESPIDF;

    class ServiceInfo
    {
        public int EntryIndex;

        public CharacteristicInfo[] characteristics;
    }

    class CharacteristicInfo
    {
        public int EntryIndex;
        public int EntryCount;

        public int ValueEntryIndex
            => EntryIndex + 1;
    }

    partial class SimulatedDevice
    {
        ServiceInfo[] services;

        OS.GattEntry[] entries;
        ushort[] interfaces;

        byte[] memory;

        #region Natives declared

        internal void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
        {
            DeviceName = deviceName;
            Mode = mode;
            MaxTransferUnit = maxTransferUnit;
        }

        internal void NativePrepareGatt(int[] characteristicCount, int numEntries, int totalBytes, int maxValueSize)
        {
            interfaces = new ushort[characteristicCount.Length];
            entries = new OS.GattEntry[numEntries];

            services = new ServiceInfo[characteristicCount.Length];

            for (int i = 0; i < services.Length; i++)
            {
                var numCharacteristics = characteristicCount[i];

                services[i] = new ServiceInfo
                {
                    characteristics = new CharacteristicInfo[numCharacteristics]
                };
            }

            memory = new byte[totalBytes];
        }

        internal void NativeBeginService(int serviceIndex, int entryIndex)
        {
            services[serviceIndex].EntryIndex = entryIndex;
        }

        internal void NativeBeginCharacteristic(int serviceIndex, int characteristicIndex, int entryIndex)
        {
            services[serviceIndex].characteristics[characteristicIndex] = new CharacteristicInfo
            {
                EntryIndex = entryIndex
            };
        }



        internal void NativeAddEntry(int index, byte[] uuid, bool autoRespond, int maxLength, int permissions, byte[] value)
        {
            entries[index] = new OS.GattEntry
            {
                AutoRespond = autoRespond,
                MaxLength = maxLength,
                Permissions = (SigAttributeProperties) permissions,
                UUID = uuid,
                Value = value,
            };
        }

        internal byte[] NativeGetValue(int serviceIndex, int characteristicIndex)
        {
            var entry = GetValueEntry(serviceIndex, characteristicIndex);

            return entry.Value;
        }

        #endregion


        #region Handlers

        void OnGapEvent(GapEvent e)
        {

        }

        void OnGattEvent(GattEvent e)
        {
            switch (e)
            {
                case GattRegistered registered:
                    
                    break;

                default:
                    break;
            }
        }


        #endregion

        CharacteristicInfo GetCharacteristic(int serviceIndex, int characteristicIndex)
            => services[serviceIndex].characteristics[characteristicIndex];

        OS.GattEntry GetValueEntry(int serviceIndex, int characteristicIndex)
        {
            var info = GetCharacteristic(serviceIndex, characteristicIndex);

            return entries[info.ValueEntryIndex];
        }

    }
}