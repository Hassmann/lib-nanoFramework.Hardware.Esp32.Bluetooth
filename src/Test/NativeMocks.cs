namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	using Test;

	partial class BluetoothHost
	{
		internal static SimulatedDevice Target => SimulatedDevice.Current;

		private static void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
			=> Target.NativeInitializeDevice(deviceName, mode, maxTransferUnit);

		private static void NativePrepareGatt(int[] characteristicCount, int numEntries, int totalBytes, int maxValueSize)
			=> Target.NativePrepareGatt(characteristicCount, numEntries, totalBytes, maxValueSize);

		private static void NativeAddEntry(int index, byte[] uuid, bool autoRespond, int maxLength, int permissions, byte[] value)
			=> Target.NativeAddEntry(index, uuid, autoRespond, maxLength, permissions, value);

		private static byte[] NativeGetValue(int serviceIndex, int characteristicIndex)
			=> Target.NativeGetValue(serviceIndex, characteristicIndex);

		private static void NativeBeginService(int serviceIndex, int entryIndex)
			=> Target.NativeBeginService(serviceIndex, entryIndex);

		private static void NativeBeginCharacteristic(int serviceIndex, int characteristicIndex, int entryIndex)
			=> Target.NativeBeginCharacteristic(serviceIndex, characteristicIndex, entryIndex);
	}
}