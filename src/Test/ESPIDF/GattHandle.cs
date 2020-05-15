using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ESPIDF
{
	class GattHandle
	{
		static int nextID;

		public int ID { get; private set; } = nextID++;

		public int EntryIndex { get; private set; }

		public GattHandle(int entryIndex)
		{
			EntryIndex = entryIndex;
		}
	}
}
