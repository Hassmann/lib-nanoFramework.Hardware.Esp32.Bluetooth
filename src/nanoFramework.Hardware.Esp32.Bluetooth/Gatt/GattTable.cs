using System;
using System.Collections;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
    class AttributeRow
    {
        public bool AutoRespond { get; set; } = true;

        public byte[] UUID { get; set; }

        public int Permissions { get; set; }
        public int MaxLength { get; set; }

        public byte[] Value { get; set; }
    }

    internal class GattTable
    {
        private ArrayList services;
        private ArrayList rows = new ArrayList();
        private ushort nextHandle = 1;
        BluetoothHostConfiguration configuration;

        public int NumRows { get; private set; }

        public GattTable(BluetoothHostConfiguration configuration, ArrayList services)
        {
            this.configuration = configuration;
            this.services = services;

            foreach (GattService service in services)
            {
                //// Service
                //var serviceAttribute = GetServiceAttribute(service);

                //rows.Add(new AttributeRow
                //{
                //    UUID = BitConverter.GetBytes((ushort)SigAttributeType.PrimaryService),
                //    MaxLength = 16,
                //    Value = serviceAttribute.UUID,
                //});

                //// Characteristics
                //foreach (GattCharacteristicAttribute characteristicAttribute in GetCharacteristicAttributes(service))
                //{

                //}
                

            }


        }

        private IEnumerable GetCharacteristicAttributes(GattService service)
        {
            var methods = service.GetType().GetMethods();

            foreach (var property in service.GetType().GetMethods())
            {

            }

            return null;

        }

        byte[] GetUShortBytes(int value) => BitConverter.GetBytes((ushort)value);


        private void AddCharacteristicRows(SigCharacteristic characteristic, SigAttributeProperties properties, string value)
        {

        }

        private void AddCharacteristicRows(SigCharacteristic characteristic, SigAttributeProperties properties, ushort value)
        {

        }

        private ushort AddServiceRow(SigService serviceUUID)
        {
            return AddRow(new Row
            {
                Type = BitConverter.GetBytes((ushort)SigAttributeType.PrimaryService),
                Value = BitConverter.GetBytes((ushort)serviceUUID),
            }); ;
        }

        private ushort AddRow(Row row)
        {
            var handle = nextHandle++;

            row.Handle = handle;

            rows.Add(row);

            return handle;
        }

        private class Row
        {
            public ushort Handle { get; set; }
            public byte[] Type { get; set; }
            public byte[] Value { get; set; }
            public byte[] Permissions { get; set; }
        }
    }
}