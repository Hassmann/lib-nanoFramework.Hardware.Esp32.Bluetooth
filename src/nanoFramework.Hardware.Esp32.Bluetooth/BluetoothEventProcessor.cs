using nanoFramework.Runtime.Events;
using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	public class BluetoothEventProcessor : IEventProcessor
	{
		static BluetoothEventProcessor instance;

		BaseEvent IEventProcessor.ProcessEvent(uint data1, uint data2, DateTime time)
			=> new BluetoothEvent
			{
				Time = time,
				ServiceIndex = (int)(data1 >> 16),
				CharacteristicIndex = (int)(data1 & 0xFFFF),
				EventType = (BluetoothEventType)(data2 >> 16),
				Value = (int)(data2 & 0xFFFF),
			};

		public static void Initialize()
		{
			instance = new BluetoothEventProcessor();

			EventSink.AddEventProcessor(OS.BluetoothEventCategory, instance);
		}
	}
}