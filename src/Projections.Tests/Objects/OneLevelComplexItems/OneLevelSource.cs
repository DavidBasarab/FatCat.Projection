using System;

namespace FatCat.Projections.Tests.Objects.OneLevelComplexItems
{
	public class OneLevelSource
	{
		public SubObject DifferentSubObject { get; set; }

		public SubObject SubObject { get; set; }

		public TimeSpan Time { get; set; }

		public int TimesCreated { get; set; }
	}
}