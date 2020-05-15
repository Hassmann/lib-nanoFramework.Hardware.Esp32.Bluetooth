using System.Runtime.CompilerServices;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	partial class BluetoothHost
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NativeInitializeDevice(string deviceName, BluetoothMode mode, int maxTransferUnit);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NativePrepareGatt(int[] characteristicCount, int numEntries, int totalBytes, int maxValueSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NativeBeginService(int serviceIndex, int entryIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NativeBeginCharacteristic(int serviceIndex, int characteristicIndex, int entryIndex, int entryCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NativeAddEntry(int index, byte[] uuid, bool autoRespond, int maxLength, int permissions, byte[] value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NativeFinalizeService(int serviceIndex, int totalEntryCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] NativeGetValue(int serviceIndex, int characteristicIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NativeSetValue(int serviceIndex, int characteristicIndex, byte[] data);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NativeAdvertise(byte[] data, int mode, int filter);
	}
}