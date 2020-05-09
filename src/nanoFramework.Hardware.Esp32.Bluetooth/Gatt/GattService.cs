using System;

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
    }
}