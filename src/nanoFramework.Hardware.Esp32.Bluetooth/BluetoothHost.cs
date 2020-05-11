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

		public GattService[] Services { get; private set; }

		private BluetoothHostConfiguration configuration;

		#region Initialization

		private BluetoothHost(BluetoothHostConfiguration configuration, GattService[] services)
		{
			this.configuration = configuration;

			Services = services;

			// Create Indices
			int serviceIndex = 0;

			foreach (GattService service in Services)
			{
				service.Index = serviceIndex++;

				int characteristicIndex = 0;

				foreach (GattCharacteristic characteristic in service.Characteristics)
				{
					characteristic.Index = characteristicIndex++;
				}
			}

			EventSink.AddEventListener(OS.BluetoothEventCategory, this);
		}

		public static BluetoothHost Initialize(BluetoothHostConfiguration configuration, params GattService[] services)
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

			return new BluetoothHost(configuration, services);
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
			var service = Services[e.ServiceIndex];
			var characteristic = service[e.CharacteristicIndex];

			switch (e.EventType)
			{
				case BluetoothEventType.ValueWritten:

					var value = NativeGetValue(e.ServiceIndex, e.CharacteristicIndex);

					characteristic.UpdateValue(value);

					characteristic.FireValueChange();

					break;
				default:
					break;
			}
		}



		#endregion

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
			#region Pass 1 - Requirements

			int numEntries = 0;
			int totalBytes = 0;
			int maxValueSize = 0;

			int[] characteristicCount = new int[Services.Length];

			foreach (GattService service in Services)
			{
				characteristicCount[service.Index] = service.Characteristics.Length;

				foreach (GattCharacteristic characteristic in service.Characteristics)
				{
					// Largest buffer needed
					if (characteristic.MaxDataSize > maxValueSize)
					{
						maxValueSize = characteristic.MaxDataSize;
					}
				}

				numEntries += service.EntryCount;
				totalBytes += service.MaxDataSize;
			}

			// Prepare device
			NativePrepareGatt(characteristicCount, numEntries, totalBytes, maxValueSize);

			#endregion

			#region Pass 2 - Send entries and values


			int entryIndex = 0;

			void AddEntry(OS.GattEntry entry)
				=> NativeAddEntry(entryIndex++, entry.UUID.Bytes, entry.AutoRespond, entry.MaxLength, (int)entry.Permissions, entry.Value);

			void AddEntries(OS.GattEntry[] entries)
			{
				foreach (var entry in entries)
				{
					AddEntry(entry);
				}
			}

			foreach (GattService service in Services)
			{
				NativeBeginService(service.Index, entryIndex);

				AddEntries(service.ServiceEntries);

				foreach (var characteristic in service.Characteristics)
				{
					NativeBeginCharacteristic(service.Index, characteristic.Index, entryIndex);

					AddEntries(characteristic.Entries);
				}
			}

			#endregion
		}

	}
}