using System;
using System.Collections;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
    internal class GattTable
    {
        private ArrayList services;
        private ArrayList entries = new ArrayList();
        private ushort nextHandle = 1;
        BluetoothHostConfiguration configuration;

        public GattTable(BluetoothHostConfiguration configuration, ArrayList services)
        {
            this.configuration = configuration;
            this.services = services;

            ushort handle;
            // GAP
            AddServiceRow(SigService.GenericAccess);
            AddCharacteristicRows(
                SigCharacteristic.DeviceName,
                SigAttributeProperties.Read,
                configuration.DeviceName);
            AddCharacteristicRows(
                SigCharacteristic.Appearance,
                SigAttributeProperties.Read,
                (ushort)configuration.Appearance);

            // GATT

            entries.Add(new Row
            {
            });
        }

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

            entries.Add(row);

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