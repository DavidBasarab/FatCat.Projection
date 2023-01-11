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

		object destinationCopy = destination.DeepCopy();

		Projection.ProjectTo(ref destinationCopy, source);

		var compareObject = destinationCopy as Destination;

		compareObject.MoreData
					.Should()
					.Be(source.MoreData);

		compareObject.Id
					.Should()
					.Be(destination.Id);

		compareObject.SomeData
					.Should()
					.Be(source.SomeData);

		compareObject.NoOnSource
					.Should()
					.Be(destination.NoOnSource);

		compareObject.SubClass
					.Should()
					.NotBeNull();

		compareObject.SubClass
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