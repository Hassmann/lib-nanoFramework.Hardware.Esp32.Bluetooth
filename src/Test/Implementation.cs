using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;

namespace Test
{
    using ESPIDF;

    struct ServiceInfo
    {
        public int Index;
        public int CharacteristicCount;
    }

    struct CharacteristicInfo
    {
        public int Index;
        public int EntryCount;

        public int ValueIndex
            => Index + 1;
    }

    partial class SimulatedDevice
    {
        ServiceInfo[] services;
        CharacteristicInfo[] characteristics;

        OS.GattEntry[] attributes;
        ushort[] interfaces;

        byte[] memory;

        #region Natives declared
        
        internal void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
        {
            DeviceName = deviceName;
            Mode = mode;
            MaxTransferUnit = maxTransferUnit;
        }

        internal void NativePrepareGatt(int numServices, int numEntries, int totalBytes)
        {
            interfaces = new ushort[numServices];
            attributes = new OS.GattEntry[numEntries];
            services = new ServiceInfo[numServices];

            memory = new byte[totalBytes];
        }

        internal void NativeAddEntry(int index, byte[] uuid, bool autoRespond, int maxLength, int permissions, byte[] value)
        {
            attributes[index] = new OS.GattEntry
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


        OS.GattEntry GetValueEntry(int serviceIndex, int characteristicIndex)
        {
            var info = characteristics[services[serviceIndex].Index + characteristicIndex];

            return attributes[info.ValueIndex];
        }

    }
}