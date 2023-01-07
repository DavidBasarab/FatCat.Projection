using FatCat.Fakes;
using FatCat.Toolkit.Extensions;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.BasicProjection;

public class ProjectionToExistingObjectWithoutSameProperty
{
	[Fact]
	public void CanProjectToAClassWithoutDataInSourceAndDestinationDataStays()
	{
		var destination = Faker.Create<Destination>();
		var source = Faker.Create<Source>();

		var destinationCopy = destination.DeepCopy();

		var result = Projection.ProjectTo(destinationCopy, source) as Destination;

		ReferenceEquals(destinationCopy, result)
			.Should()
			.BeTrue();

		result.MoreData
			.Should()
			.Be(source.MoreData);

		result.Id
			.Should()
			.Be(destination.Id);

		result.SomeData
			.Should()
			.Be(source.SomeData);

		result.NoOnSource
			.Should()
			.Be(destination.NoOnSource);
	}

	private class Destination
	{
		public int Id { get; set; }

		public string MoreData { get; set; }

		public string NoOnSource { get; set; }

		public string SomeData { get; set; }
	}

	private class Source
	{
		public string MoreData { get; set; }

		public string SomeData { get; set; }
	}
}