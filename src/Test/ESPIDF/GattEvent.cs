using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ESPIDF
{
	class GattEvent
	{
	}

	class GattRegistered : GattEvent
	{
		public bool Success { get; set; }
		public int AppID { get; set; }
	}

	class GattWrite : GattEvent
	{
		public bool NeedsResponse { get; set; }
		public bool IsPrepare { get; set; }
		public byte[] Value { get; set; }
	}

	class GattExecuteWrite : GattEvent
	{
	}

	class GattConnected : GattEvent
	{
	}

	class GattDisconnected : GattEvent
	{
	}

	class GattTableCreated : GattEvent
	{
	}

}
