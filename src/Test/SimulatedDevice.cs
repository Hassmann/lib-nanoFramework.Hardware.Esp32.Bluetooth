using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;
using Test.ESPIDF;

namespace Test
{
	internal partial class SimulatedDevice
	{
		public static SimulatedDevice Current { get; private set; }

		public string DeviceName { get; private set; }

		public BluetoothMode Mode { get; private set; }

		public int MaxTransferUnit { get; private set; }

		public static SimulatedDevice Reset() => Current = new SimulatedDevice();

		#region ESP Bluetooth API

		private GattInterface[] interfaces = new GattInterface[16];

		private void esp_ble_gatts_app_register(int serviceID)
		{
			interfaces[serviceID] = new GattInterface(serviceID);

			OnGattEvent(interfaces[serviceID], new ESPIDF.GattRegistered { AppID = serviceID });
		}

		private void esp_ble_gap_config_adv_data_raw(byte[] data)
		{
			OnGapEvent(new GapAdvertisingDataComplete());
		}

		private void esp_ble_gap_config_scan_rsp_data_raw(byte[] data)
		{
			OnGapEvent(new GapResponseDataComplete());
		}

		private void esp_ble_gap_start_advertising(esp_ble_adv_params_t advertisingParameters)
		{
		}

		private void esp_ble_gap_set_device_name(string deviceName)
			=> DeviceName = deviceName;

		private void esp_ble_gatts_create_attr_tab(int startEntry, int entryCount, GattInterface git, int appID)
		{
			var handles = new GattHandle[entryCount];

			for (int i = 0; i < entryCount; i++)
			{
				handles[i] = new GattHandle(startEntry + i);
			}

			OnGattEvent(git, new GattTableCreated
			{
				handles = handles,
				serviceID = appID,
			});
		}

		private void esp_ble_gatts_start_service(GattHandle gattHandle)
		{
		}

		#endregion ESP Bluetooth API

		internal void TestSetString(GattService service, TextCharacteristic textCharacteristic, string value)
		{
			var git = services[service.Index].git;
			var entryIndex = services[service.Index].characteristics[textCharacteristic.Index].ValueEntryIndex;
			var handle = handles[entryIndex].handle;

			// Write to store
			var entry = GetValueEntry(service.Index, textCharacteristic.Index);

			entry.Value = OS.Encode(value);

			// Callback
			OnGattEvent(git, new GattWrite
			{
				handle = handle,
				Value = OS.Encode(value),
			});
		}
	}
}