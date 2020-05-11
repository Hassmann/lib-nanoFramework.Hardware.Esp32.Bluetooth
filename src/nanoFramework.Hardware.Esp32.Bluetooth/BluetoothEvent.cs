using nanoFramework.Runtime.Events;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	enum BluetoothEventType
	{
		ValueWritten
	}

	internal class BluetoothEvent : BaseEvent
	{
		public BluetoothEventType EventType { get; set; }

		public int ServiceIndex { get; set; }
		public int CharacteristicIndex { get; set; }
		
		public int Value { get; set; }
	}
}