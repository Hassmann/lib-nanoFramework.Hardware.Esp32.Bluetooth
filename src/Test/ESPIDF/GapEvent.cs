namespace Test.ESPIDF
{
	internal class GapEvent
	{
		public bool Success { get; set; } = true;
	}

	internal class GapAdvertisingDataComplete : GapEvent
	{
	}

	internal class GapResponseDataComplete : GapEvent
	{
	}
}