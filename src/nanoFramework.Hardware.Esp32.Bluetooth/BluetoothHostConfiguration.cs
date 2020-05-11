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
		public SigDeviceAppearance Appearance { get; set; } = SigDeviceAppearance.Unknown;
		public int MaxTransferUnit { get; set; } = 500;
	}
}