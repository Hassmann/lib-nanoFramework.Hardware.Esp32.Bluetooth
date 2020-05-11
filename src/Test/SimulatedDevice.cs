using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;
using nanoFramework.Runtime.Events;

namespace Test
{
	internal partial class SimulatedDevice
	{
		public static SimulatedDevice Current { get; private set; }

		public string DeviceName { get; private set; }

		public BluetoothMode Mode { get; private set; }

		public int MaxTransferUnit { get; private set; }

		public static SimulatedDevice Reset() => Current = new SimulatedDevice();


		#region Bluetooth Stack





		#endregion


		internal void TestSetString(GattService service, TextCharacteristic textCharacteristic, string value)
		{
			// Write to store
			var entry = GetValueEntry(service.Index, textCharacteristic.Index);

			entry.Value = OS.Encode(value);

			// Call Handler
			EventSink.Fire(new BluetoothEvent
			{
				EventType = BluetoothEventType.ValueWritten,
				ServiceIndex = service.Index,
				CharacteristicIndex = textCharacteristic.Index,
			});
		}
	}
}