using Microsoft.VisualStudio.TestTools.UnitTesting;
using nanoFramework.Hardware.Esp32.Bluetooth;
using nanoFramework.Hardware.Esp32.Bluetooth.Gatt;
using System;

namespace Test
{
	[TestClass]
	public class BasicTests
	{
		private const string deviceName = "Test Device";

		private const string serviceUUID = "E6B4E8D6-00F2-44EE-B78D-4CF4330922BE";
		private const string textUUID = "EAD840EE-4F73-4AC7-ACCA-13F8229D08D7";

		private const int MaxStringLength = 10;
		private static DateTime TestBegin = DateTime.UtcNow;

		private SimulatedDevice device;
		private BluetoothHost host;
		private GattService service;
		private Advertisement advertisement;

		#region Initialization

		[TestInitialize]
		public void BeforeEach()
		{
			device = SimulatedDevice.Reset();

			service = CreateTestService();

			host = BluetoothHost.Initialize(new BluetoothHostConfiguration(deviceName), service);

			advertisement = new Advertisement(
					new Advertisement.DataFragment(SigAdvertisingDataType.CompleteListof128bitServiceClassUUIDs, service.UUID.Bytes)
				);
		}

		[TestCleanup]
		public void AfterEach()
		{
		}

		private GattService CreateTestService()
			=> new GattService(serviceUUID,
				new TimeCharacteristic(
					"Time", TestBegin,
					SigCharacteristic.CurrentTime,
					SigAttributeProperties.Read),
				new TextCharacteristic(
					"Text", "Original",
					MaxStringLength,
					textUUID,
					SigAttributeProperties.Read | SigAttributeProperties.WriteWithoutResponse)
				);

		#endregion Initialization

		[TestMethod]
		public void Host_Initialized()
		{
			Assert.AreEqual(deviceName, device.DeviceName);
		}

		[TestMethod]
		public void Service_Initialized()
		{
			var timeCharacteristic = service["Time"] as TimeCharacteristic;
			Assert.IsNotNull(timeCharacteristic);
			Assert.AreSame(timeCharacteristic, service[0]);

			var textCharacteristic = service["Text"] as TextCharacteristic;
			Assert.IsNotNull(textCharacteristic);
			Assert.AreSame(textCharacteristic, service[1]);
		}

		[TestMethod]
		public void Service_Started()
		{
			host.Advertise(advertisement);

			var timeCharacteristic = service["Time"] as TimeCharacteristic;

			var stored = device.NativeGetValue(timeCharacteristic.ServiceIndex, timeCharacteristic.Index);
			Assert.AreEqual(timeCharacteristic.Value, TimeCharacteristic.Decode(stored));

			var textCharacteristic = service["Text"] as TextCharacteristic;

			stored = device.NativeGetValue(textCharacteristic.ServiceIndex, textCharacteristic.Index);
			Assert.AreEqual(textCharacteristic.Value, TextCharacteristic.Decode(stored));
		}

		[TestMethod]
		public void Service_SetTextValue()
		{
			host.Advertise(advertisement);

			var text = service["Text"] as TextCharacteristic;

			text.Value = "Test";

			var stored = device.NativeGetValue(text.ServiceIndex, text.Index);
			Assert.AreEqual("Test", OS.Decode(stored));
		}

		[TestMethod]
		public void Service_Notify()
		{
			var textCharacteristic = service["Text"] as TextCharacteristic;

			bool gotNotified = false;

			textCharacteristic.ValueChanged += (characteristic) =>
			{
				Assert.AreSame(textCharacteristic, characteristic);
				gotNotified = true;
			};

			host.Advertise(advertisement);

			BluetoothHost.Target.TestSetString(service, textCharacteristic, "Test");

			Assert.IsTrue(gotNotified);
			Assert.AreEqual("Test", textCharacteristic.Value);
		}
	}
}