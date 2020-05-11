using nanoFramework.Runtime.Events;
using System;
using System.Collections;


namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	using Gatt;

	public partial class BluetoothHost : IEventListener
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

		#region Event system

		void IEventListener.InitializeForEventSource()
		{

		}

		bool IEventListener.OnEvent(BaseEvent e)
		{
			if (e is BluetoothEvent be)
			{
				OnEvent(be);
				return true;
			}

			return false;
		}

		private void OnEvent(BluetoothEvent e)
		{
			var service = (GattService)services[e.ServiceIndex];
			var characteristic = service[e.CharacteristicIndex];

			switch (e.EventType)
			{
				case BluetoothEventType.ValueWritten:
					var value = NativeGetValue(e.ServiceIndex, e.CharacteristicIndex);

					characteristic.UpdateValue(value);

					break;
				default:
					break;
			}
		}



		#endregion

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
				throw new NotImplementedException();
			}
		}

		private void BuildTable()
		{
			// Pass 1 - Requirements and Indices
			int numEntries = 0;
			int totalBytes = 0;

			int serviceIndex = 0;

			foreach (GattService service in services)
			{
				service.Index = serviceIndex++;

				foreach (GattCharacteristic characteristic in service.Characteristics)
				{

				}
				numEntries += service.EntryCount;
				totalBytes += service.MaxDataSize;
			}

			// Prepare device
			NativePrepareGatt(services.Count, numEntries, totalBytes);

			// Send entries and values

			int index = 0;

			foreach (GattService service in services)
			{
				foreach (OS.GattEntry entry in service.Entries)
				{
					NativeAddEntry(index++, entry.UUID.Bytes, entry.AutoRespond, entry.MaxLength, (int)entry.Permissions, entry.Value);
				}
			}
		}

	}
}