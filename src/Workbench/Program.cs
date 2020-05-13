using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;
using System;
using System.Threading;

namespace Workbench
{
	public class Program
	{
		private const string deviceName = "Test Device";

		private static GattID serviceUUID = "E6B4E8D6-00F2-44EE-B78D-4CF4330922BE";
		private static GattID textUUID = "EAD840EE-4F73-4AC7-ACCA-13F8229D08D7";

		private static BluetoothHost host;

		// Definition of the GATT service(s)
		private static GattService service = new GattService(serviceUUID,
			new TimeCharacteristic(
				"Time", DateTime.UtcNow,
				SigCharacteristic.CurrentTime,
				SigAttributeProperties.Read),
			new TextCharacteristic(
				"Text", "Hello nanoFramework!",
				50,
				textUUID,
				SigAttributeProperties.Read | SigAttributeProperties.WriteWithoutResponse)
		);

		// Advertising data definition
		private static Advertisement advertisement = new Advertisement(
			new Advertisement.DataFragment(
				SigAdvertisingDataType.CompleteListof128bitServiceClassUUIDs, serviceUUID.Bytes)
		);

		// Some ways to access a characteristic
		private static TextCharacteristic TextCharacteristic
			=> service["Text"] as TextCharacteristic;

		private static string Text
		{
			get => TextCharacteristic.Value;
			set => TextCharacteristic.Value = value;
		}

		public static void Main()
		{
			host = BluetoothHost.Initialize(new BluetoothHostConfiguration(deviceName), service);

			TextCharacteristic.ValueChanged += (characteristic) =>
			{
				Console.WriteLine($"Value changed: {TextCharacteristic.Name} = {TextCharacteristic.Value}");
			};

			host.Advertise(advertisement);

			Thread.Sleep(Timeout.Infinite);
		}
	}
}