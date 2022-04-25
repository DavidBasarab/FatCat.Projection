using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.BasicProjection;

public class MultiLevelObjectProjection
{
	[Fact]
	public void ObjectsAreNewlyCreated()
	{
		var source = Faker.Create<MultiLevelObjectSource>();

		var destination = Projection.ProjectTo<MultiLevelObjectSource>(source);

		ReferenceEquals(source, destination)
			.Should()
			.BeFalse();
	}

	[Fact]
	public void ProjectToAMultiLevelObjectDestination()
	{
		var source = Faker.Create<MultiLevelObjectSource>();

		var destination = Projection.ProjectTo<MultiLevelObjectDestination>(source);

		destination.CreatedTime
					.Should()
					.Be(source.CreatedTime);

		destination.Name
					.Should()
					.Be(source.Name);

		destination.Level1.Time
					.Should()
					.Be(source.Level1.Time);

		destination.Level1.TimesCreated
					.Should()
					.Be(2);

		destination.Level1.DifferentSubObject.Number
					.Should()
					.Be(source.Level1.DifferentSubObject.Number);

		destination.Level1.DifferentSubObject.UpdatedDate
					.Should()
					.Be(source.Level1.DifferentSubObject.UpdatedDate);

		destination.Level1.SubObject
					.Should()
					.BeEquivalentTo(source.Level1.SubObject);
	}

	[Fact]
	public void ProjectToAMultiLevelObjectSameObject()
	{
		var multiLevelObject = Faker.Create<MultiLevelObjectSource>();

		var result = Projection.ProjectTo<MultiLevelObjectSource>(multiLevelObject);

		result
			.Should()
			.BeEquivalentTo(multiLevelObject);
	}
}