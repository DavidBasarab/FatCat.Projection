using System;

namespace FatCat.Projections.Tests.Objects.OneLevelComplexItems
{
	public class OneLevelDestination
	{
		public SubObject SubObject { get; set; }

		public DifferentSubObject DifferentSubObject { get; set; }

		public TimeSpan Time { get; set; }

		public int TimesCreated => 2;
	}
}