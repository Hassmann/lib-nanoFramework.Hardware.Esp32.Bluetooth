using System.Text;

namespace nanoFramework.Hardware.Esp32.Bluetooth
{
	using Gatt;
	using System;

	public static class OS
	{
		public static byte[] Encode(string value)
			=> value != null ? Encoding.UTF8.GetBytes(value) : null;

		public struct GattEntry
		{
			public bool AutoRespond;
			public GattID UUID;
			public SigAttributeProperties Permissions;
			public int MaxLength;
			public byte[] Value;
		}

		public static class Size
		{
			public const int Time = 8;
			public const int Character = 2;
		}

		internal static string Decode(byte[] value)
			=> value != null ? Encoding.UTF8.GetString(value, 0, value.Length) : null;
	}
}