namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    using Gatt;

    public class BluetoothHostConfiguration
    {
        public BluetoothHostConfiguration(string deviceName)
        {
            DeviceName = deviceName;
        }

        public string DeviceName { get; set; }

        public BluetoothMode Mode { get; set; } = BluetoothMode.LowEnergy;
        public int MaxTransferUnit { get; set; } = 500;
    }

    public partial class BluetoothHost
    {
        private BluetoothHost()
        {
            NativeCheckInterop("Hello ESP32");
        }

        public static BluetoothHost Initialize(BluetoothHostConfiguration configuration)
        {
            return new BluetoothHost();
        }

        public void AddService(GattService service)
        {
        }

        public void Advertise(bool enable = true)
        {
        }
    }
}