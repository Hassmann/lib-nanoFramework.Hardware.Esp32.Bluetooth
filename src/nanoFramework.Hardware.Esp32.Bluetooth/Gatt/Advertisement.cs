using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
	public class Advertisement
	{
		public const int MaxDataLength = 31;

		public byte[] Data { get; private set; }

		public AdvertisingMode Mode { get; set; } = AdvertisingMode.General;
		public AdvertisingFilter Filter { get; set; } = AdvertisingFilter.AnyScanAnyConnection;

		public Advertisement(params DataFragment[] fragments)
		{
			int totalLength = 0;

			foreach (var fragment in fragments)
			{
				totalLength += fragment.TotalLength;
			}

			if (totalLength > MaxDataLength)
			{
				throw new ArgumentException("Data too large", "fragments");
			}

			Data = new byte[totalLength];

			int offset = 0;

			foreach (var fragment in fragments)
			{
				offset += fragment.CopyTo(Data, offset);
			}
		}

		public class DataFragment
		{
			public SigAdvertisingDataType DataType { get; private set; }
			public byte[] Data { get; private set; }
			
			public int TotalLength => 2 + Data.Length;

			public DataFragment(SigAdvertisingDataType dataType, byte[] data)
			{
				DataType = dataType;
				Data = data;
			}

			internal int CopyTo(byte[] destination, int offset)
			{
				destination[offset] = (byte)(1 + Data.Length);
				destination[offset + 1] = (byte)(DataType);

				Array.Copy(
					Data, 0,
					destination, offset + 2,
					Data.Length);

				return TotalLength;
			}
		}
	}
}
