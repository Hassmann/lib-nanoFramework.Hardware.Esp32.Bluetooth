using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;

namespace Test
{
	using ESPIDF;
	using nanoFramework.Runtime.Events;
	using System;

	internal class ServiceInfo
	{
		public int EntryIndex;
		public GattInterface git;
		public CharacteristicInfo[] characteristics;
		internal int TotalEntryCount;
	}

	internal class CharacteristicInfo
	{
		public int EntryIndex;
		public int EntryCount;

		public int ValueEntryIndex
			=> EntryIndex + 1;
	}

	internal class HandleInfo
	{
		public GattHandle handle;
		public int serviceIndex;
		public int characteristicIndex;
		public int entryOffset;
	}

	partial class SimulatedDevice
	{
		private ServiceInfo[] services;

		private OS.GattEntry[] entries;
		private HandleInfo[] handles;

		private byte[] memory;

		private esp_ble_adv_params_t advertisingParameters;

		private bool isAdvertisingDataComplete;
		private bool isResponseDataComplete;

		#region Natives declared

		internal void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
		{
			Mode = mode;
			MaxTransferUnit = maxTransferUnit;

			esp_ble_gap_set_device_name(deviceName);
		}

		internal void NativePrepareGatt(int[] characteristicCount, int numEntries, int totalBytes, int maxValueSize)
		{
			entries = new OS.GattEntry[numEntries];
			handles = new HandleInfo[numEntries];

			services = new ServiceInfo[characteristicCount.Length];

			for (int i = 0; i < services.Length; i++)
			{
				var numCharacteristics = characteristicCount[i];

				services[i] = new ServiceInfo
				{
					characteristics = new CharacteristicInfo[numCharacteristics]
				};
			}

			memory = new byte[totalBytes];
		}

		internal void NativeBeginService(int serviceIndex, int entryIndex)
		{
			services[serviceIndex].EntryIndex = entryIndex;
			handles[entryIndex] = new HandleInfo
			{
				serviceIndex = serviceIndex,
				characteristicIndex = -1,
				entryOffset = 0,
			};
		}

		internal void NativeBeginCharacteristic(int serviceIndex, int characteristicIndex, int entryIndex, int entryCount)
		{
			services[serviceIndex].characteristics[characteristicIndex] = new CharacteristicInfo
			{
				EntryIndex = entryIndex,
				EntryCount = entryCount,
			};

			for (int i = 0; i < entryCount; i++)
			{
				handles[entryIndex + i] = new HandleInfo
				{
					serviceIndex = serviceIndex,
					characteristicIndex = characteristicIndex,
					entryOffset = i,
				};
			}
		}

		internal void NativeAddEntry(int index, byte[] uuid, bool autoRespond, int maxLength, int permissions, byte[] value)
		{
			entries[index] = new OS.GattEntry
			{
				AutoRespond = autoRespond,
				MaxLength = maxLength,
				Permissions = (SigAttributeProperties)permissions,
				UUID = uuid,
				Value = value,
			};
		}

		internal void NativeFinalizeService(int serviceIndex, int totalEntryCount)
		{
			services[serviceIndex].TotalEntryCount = totalEntryCount;

			esp_ble_gatts_app_register(serviceIndex);
		}

		internal byte[] NativeGetValue(int serviceIndex, int characteristicIndex)
		{
			var entry = GetValueEntry(serviceIndex, characteristicIndex);

			return entry.Value;
		}

		internal void NativeSetValue(int serviceIndex, int characteristicIndex, byte[] data)
		{
			var entry = GetValueEntry(serviceIndex, characteristicIndex);

			entry.Value = data;
		}

		internal void NativeAdvertise(byte[] data, int mode, int filter)
		{
			advertisingParameters = new esp_ble_adv_params_t
			{
				mode = (AdvertisingMode)mode,
				filter = (AdvertisingFilter)filter,
			};

			esp_ble_gap_config_adv_data_raw(data);
			esp_ble_gap_config_scan_rsp_data_raw(data);
		}

		#endregion Natives declared

		#region Handlers

		private void OnGapEvent(GapEvent e)
		{
			switch (e)
			{
				case GapAdvertisingDataComplete adComplete:
					isAdvertisingDataComplete = true;
					if (isResponseDataComplete)
					{
						esp_ble_gap_start_advertising(advertisingParameters);
					}
					break;

				case GapResponseDataComplete rspComplete:
					isResponseDataComplete = true;
					if (isAdvertisingDataComplete)
					{
						esp_ble_gap_start_advertising(advertisingParameters);
					}
					break;

				default:
					break;
			}
		}

		private void OnGattEvent(GattInterface git, GattEvent e)
		{
			switch (e)
			{
				case GattRegistered registered:
					{
						var service = services[registered.AppID];
						service.git = git;

						var firstIndex = service.EntryIndex;

						esp_ble_gatts_create_attr_tab(firstIndex, service.TotalEntryCount, git, registered.AppID);
					}
					break;

				case GattTableCreated table:
					{
						var service = services[table.serviceID];
						var serviceIndex = service.EntryIndex;

						for (int i = 0; i < table.handles.Length; i++)
						{
							handles[serviceIndex + i].handle = table.handles[i];
						}

						esp_ble_gatts_start_service(handles[service.EntryIndex].handle);
					}
					break;

				case GattWrite write:
					{
						var handle = FindHandle(write.handle);

						var (data1, data2) = CreateEventData(handle.serviceIndex, handle.characteristicIndex, BluetoothEventType.ValueWritten, (ushort)handle.entryOffset);
						EventSink.Fire(data1, data2, DateTime.UtcNow);
					}
					break;

				default:
					break;
			}
		}

		#endregion Handlers


		internal static (uint data1, uint data2) CreateEventData(int serviceIndex, int characteristicIndex, BluetoothEventType eventType, ushort value)
			=> ((uint)((serviceIndex << 16) + characteristicIndex), (uint)((((int)eventType) << 16) + value));

		internal HandleInfo FindHandle(GattHandle handle)
		{
			foreach (var item in handles)
			{
				if (item.handle == handle)
				{
					return item;
				}
			}

			throw new IndexOutOfRangeException();
		}

		private CharacteristicInfo GetCharacteristic(int serviceIndex, int characteristicIndex)
					=> services[serviceIndex].characteristics[characteristicIndex];

		private OS.GattEntry GetValueEntry(int serviceIndex, int characteristicIndex)
		{
			var info = GetCharacteristic(serviceIndex, characteristicIndex);

			return entries[info.ValueEntryIndex];
		}
	}
}