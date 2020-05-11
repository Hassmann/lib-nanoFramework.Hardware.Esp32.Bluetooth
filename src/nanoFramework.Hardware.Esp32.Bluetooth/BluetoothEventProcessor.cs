using nanoFramework.Runtime.Events;
using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	public class BluetoothEventProcessor : IEventProcessor
	{
		static BluetoothEventProcessor instance;

		BaseEvent IEventProcessor.ProcessEvent(uint data1, uint data2, DateTime time)
		{
			throw new NotImplementedException();
		}

		public static void Initialize()
		{
			instance = new BluetoothEventProcessor();

			EventSink.AddEventProcessor(OS.BluetoothEventCategory, instance);
		}
	}
}