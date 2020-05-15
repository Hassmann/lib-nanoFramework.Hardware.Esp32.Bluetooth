namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	using Test;

	partial class BluetoothHost
	{
		internal static SimulatedDevice Target => SimulatedDevice.Current;

		internal static void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit)
			=> Target.NativeInitializeDevice(deviceName, mode, maxTransferUnit);

		internal static void NativePrepareGatt(int[] characteristicCount, int numEntries, int totalBytes, int maxValueSize)
			=> Target.NativePrepareGatt(characteristicCount, numEntries, totalBytes, maxValueSize);

		internal static void NativeBeginService(int serviceIndex, int entryIndex)
			=> Target.NativeBeginService(serviceIndex, entryIndex);

		internal static void NativeBeginCharacteristic(int serviceIndex, int characteristicIndex, int entryIndex, int entryCount)
			=> Target.NativeBeginCharacteristic(serviceIndex, characteristicIndex, entryIndex, entryCount);

		internal static void NativeAddEntry(int index, byte[] uuid, bool autoRespond, int maxLength, int permissions, byte[] value)
			=> Target.NativeAddEntry(index, uuid, autoRespond, maxLength, permissions, value);

		internal static void NativeFinalizeService(int serviceIndex, int totalEntryCount)
			=> Target.NativeFinalizeService(serviceIndex, totalEntryCount);

		internal static byte[] NativeGetValue(int serviceIndex, int characteristicIndex)
			=> Target.NativeGetValue(serviceIndex, characteristicIndex);

		internal static void NativeSetValue(int serviceIndex, int characteristicIndex, byte[] data)
			=> Target.NativeSetValue(serviceIndex, characteristicIndex, data);

		internal static void NativeAdvertise(byte[] data, int mode, int filter)
			=> Target.NativeAdvertise(data, mode, filter);
	}
}