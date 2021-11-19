using System;

namespace FatCat.Projections.Tests.Objects.SimpleItems
{
	public class SimpleSource
	{
		public string? FirstName { get; set; }

		public int Number { get; set; }

		public DateTime SomeDate { get; set; }

		public bool SomeSwitch { get; set; }

		public TestingEnum SomeEnum { get; set; }
	}
}