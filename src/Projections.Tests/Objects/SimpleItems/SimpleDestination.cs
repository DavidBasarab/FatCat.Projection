using System;

namespace FatCat.Projections.Tests.Objects.SimpleItems
{
	public class SimpleDestination
	{
		public string? FirstName { get; set; }

		public int Number { get; set; }

		public DateTime SomeDate { get; set; }

		public TestingEnum SomeEnum { get; set; }

		public bool SomeSwitch { get; set; }
	}
}