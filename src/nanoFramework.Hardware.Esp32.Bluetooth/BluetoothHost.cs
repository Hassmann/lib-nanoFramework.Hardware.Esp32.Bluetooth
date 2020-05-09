namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    using Gatt;
    using System;
    using System.Collections;
    using System.Diagnostics;

    public class BluetoothHostConfiguration
    {
        public BluetoothHostConfiguration(string deviceName)
        {
            DeviceName = deviceName;
        }

        public string DeviceName { get; set; }

        public BluetoothMode Mode { get; set; } = BluetoothMode.LowEnergy;
        public SigDeviceAppearance Appearance { get; set; } = SigDeviceAppearance.Unknown;
        public int MaxTransferUnit { get; set; } = 500;
    }

    public partial class BluetoothHost
    {
        private static bool isDeviceInitialized;

        private bool isLocked;

        private ArrayList services = new ArrayList();

        private BluetoothHostConfiguration configuration;

        #region Initialization

        private BluetoothHost(BluetoothHostConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public static BluetoothHost Initialize(BluetoothHostConfiguration configuration)
        {
            if (!isDeviceInitialized)
            {
                NativeInitializeDevice(
                    configuration.DeviceName,
                    configuration.Mode,
                    configuration.MaxTransferUnit);

                isDeviceInitialized = true;
            }
            else
            {
                // Bluetooth reinitialization is not supported yet
            }

            return new BluetoothHost(configuration);
        }

        #endregion Initialization

        public void AddService(GattService service)
        {
            if (isLocked)
            {
                throw new InvalidOperationException("Can't add service after advertising has started.");
            }

            services.Add(service);
        }

        public void Advertise(bool enable = true)
        {
            if (!isLocked)
            {
                // First time, set it all up
                BuildTable();

                isLocked = true;
            }
            else
            {
                // Just disable, (re-)enable
            }
        }

        private void BuildTable()
        {
            var table = new GattTable(configuration, services);


        }
    }
}