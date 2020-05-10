namespace nanoFramework.Hardware.Esp32.Bluetooth
{
    using Gatt;

    public static class OS
    {
        public static class Size
        {
            public const int Time = 8;
            public const int Character = 2;
        }

        public struct GattEntry
        {
            public bool AutoRespond;
            public GattID UUID;
            public SigAttributeProperties Permissions;
            public int MaxLength;
            public byte[] Value;
        }

    }
}