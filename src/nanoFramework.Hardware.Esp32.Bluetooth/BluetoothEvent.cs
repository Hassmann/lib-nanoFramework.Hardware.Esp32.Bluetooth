using nanoFramework.Runtime.Events;
using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	enum BluetoothEventType
	{
		ValueWritten
	}

	internal class BluetoothEvent : BaseEvent
	{
		public DateTime	Time { get; internal set; }
		public BluetoothEventType EventType { get; internal set; }

		public int ServiceIndex { get; internal set; }
		public int CharacteristicIndex { get; internal set; }
		
		public int Value { get; internal set; }
	}
}