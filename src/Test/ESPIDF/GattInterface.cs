using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ESPIDF
{
	class GattInterface
	{
		static int nextID;

		public int ID { get; private set; } = nextID++;

		public int ServiceIndex { get; private set; }

		public GattInterface(int serviceIndex)
		{
			ServiceIndex = serviceIndex;
		}
	}
}
