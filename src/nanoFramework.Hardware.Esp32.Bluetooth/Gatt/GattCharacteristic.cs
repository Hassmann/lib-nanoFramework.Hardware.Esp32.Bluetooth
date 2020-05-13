using System;

namespace nanoFramework.Hardware.Esp32.Bluetooth.Gatt
{
	public delegate void ToSignalCharacteristicChange(GattCharacteristic characteristic);

	public abstract class GattCharacteristic
	{
		public GattCharacteristic(string name, GattID uuid, SigAttributeProperties properties)
		{
			Name = name;
			UUID = uuid;
			Properties = properties;
		}

		public event ToSignalCharacteristicChange ValueChanged;

		public string Name { get; private set; }
		public GattID UUID { get; private set; }
		public SigAttributeProperties Properties { get; private set; }

		public abstract int MaxDataSize { get; }
		public abstract byte[] Data { get; }

		public int Index { get; internal set; }
		public int ServiceIndex { get; internal set; }

		public virtual int EntryCount => 2;

		internal virtual OS.GattEntry[] Entries => new OS.GattEntry[]
		{
			new OS.GattEntry
			{
				AutoRespond = true,
				UUID = SigAttributeType.CharacteristicDeclaration,
				MaxLength = UUID.Bytes.Length,
				Permissions = SigAttributeProperties.Read,
				Value = UUID.Bytes,
			},
			new OS.GattEntry
			{
				AutoRespond = true,
				UUID = UUID,
				MaxLength = MaxDataSize,
				Permissions = Properties,
				Value = Data,
			},
		};

		internal abstract void UpdateValue(byte[] value);

		internal void FireValueChange()
			=> ValueChanged?.Invoke(this);
	}

	public class TimeCharacteristic : GattCharacteristic
	{
		private DateTime value;

		public TimeCharacteristic(string name, DateTime value, GattID uuid, SigAttributeProperties properties) : base(name, uuid, properties)
		{
			this.value = value;
		}

		public override int MaxDataSize => OS.Size.Time;

		public DateTime Value
		{
			get => value;
			set
			{
				this.value = value;
				BluetoothHost.NativeSetValue(ServiceIndex, Index, Data);
			}
		}

		public override byte[] Data
			=> Encode(value);

		internal override void UpdateValue(byte[] data)
			=> value = Decode(data);

		#region Encoding

		public static byte[] Encode(DateTime value)
			=> BitConverter.GetBytes(value.Ticks);

		public static DateTime Decode(byte[] data)
		{
			var ticks = BitConverter.ToInt64(data, 0);
			return new DateTime(ticks);
		}

		#endregion Encoding
	}

	public class TextCharacteristic : GattCharacteristic
	{
		private string value;

		public TextCharacteristic(string name, string value, int maxLength, GattID uuid, SigAttributeProperties properties) : base(name, uuid, properties)
		{
			MaxLength = maxLength;
			this.value = value;
		}

		public string Value
		{
			get => value;

			set 
			{ 
				this.value = value;
				BluetoothHost.NativeSetValue(ServiceIndex, Index, Data);
			}
		}

		public int MaxLength { get; private set; }

		public override int MaxDataSize
			=> MaxLength * OS.Size.Character;

		public override byte[] Data
			=> OS.Encode(value);

		internal override void UpdateValue(byte[] value)
		{
			this.value = OS.Decode(value);
		}

		#region Encoding

		public static byte[] Encode(string value)
			=> OS.Encode(value);

		public static string Decode(byte[] data)
			=> OS.Decode(data);

		#endregion Encoding

	}
}