using FatCat.Fakes;
using Xunit;

namespace FatCat.Projections.Tests.BasicProjection;

public class ProjectionWithMissingProperty
{
	[Fact]
	public void CanProjectWithMissingDestinationFromSource()
	{
		var source = Faker.Create<Source>();

		var destination = Projection.ProjectTo<Destination>(source);
	}

	private class Destination
	{
		public string FirstName { get; set; }

		public string Id { get; set; }

		public string LastName { get; set; }
	}

	private class Source
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}