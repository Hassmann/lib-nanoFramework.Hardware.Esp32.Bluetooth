using System.Text;
using nanoFramework.Runtime.Events;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	using Gatt;

	internal static class OS
	{
		internal static byte[] Encode(string value)
			=> value != null ? Encoding.UTF8.GetBytes(value) : null;

		internal const EventCategory BluetoothEventCategory = EventCategory.Custom; // TODO Dedicated category

		internal struct GattEntry
		{
			internal bool AutoRespond;
			internal GattID UUID;
			internal SigAttributeProperties Permissions;
			internal int MaxLength;
			internal byte[] Value;
		}

		internal static class Size
		{
			internal const int Time = 8;
			internal const int Character = 2;
		}

		internal static string Decode(byte[] value)
			=> value != null ? Encoding.UTF8.GetString(value, 0, value.Length) : null;
	}
}