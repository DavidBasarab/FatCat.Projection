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

		Projection.ProjectTo(destinationCopy, source);

		destinationCopy.MoreData
						.Should()
						.Be(source.MoreData);

		destinationCopy.Id
						.Should()
						.Be(destination.Id);

		destinationCopy.SomeData
						.Should()
						.Be(source.SomeData);

		destinationCopy.NoOnSource
						.Should()
						.Be(destination.NoOnSource);

		destinationCopy.SubClass
						.Should()
						.NotBeNull();

		destinationCopy.SubClass
						.Data
						.Should()
						.Be(destination.SubClass.Data);
	}

	private class Destination
	{
		public int Id { get; set; }

		public string MoreData { get; set; }

		public string NoOnSource { get; set; }

		public string SomeData { get; set; }

		public DestinationSubClass SubClass { get; set; }
	}

	public class DestinationSubClass
	{
		public string Data { get; set; }
	}

	private class Source
	{
		public string MoreData { get; set; }

		public string SomeData { get; set; }
	}
}