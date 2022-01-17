using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests;

public class ObjectWithListProjections
{
	[Fact(Skip = "Need to fix list stuff first")]
	public void CanProjectWithBasicListItems()
	{
		var source = Faker.Create<SourceItemWithList>();

		var result = Projection.ProjectTo<DestinationItemWithList>(source);

		CompareLists(source.Numbers, result.Numbers);
	}

	// Check for same name source and destination where one is a list and the other is not

	[Fact(Skip = "Need to fix list stuff first")]
	public void CanProjectWithSubObjectList()
	{
		var source = Faker.Create<SourceItemWithList>();

		var result = Projection.ProjectTo<DestinationItemWithList>(source);

		CompareLists(source.SubList, result.SubList);
	}

	[Fact]
	public void ProjectWithDifferentTypes()
	{
		var source = Faker.Create<SourceItemWithList>();

		var result = Projection.ProjectTo<DestinationItemWithList>(source);
	}

	private static void CompareLists<T>(IEnumerable<T> sourceList, IEnumerable<T> resultList)
	{
		sourceList
			.Should()
			.BeEquivalentTo(resultList);

		ReferenceEquals(sourceList, resultList)
			.Should()
			.BeFalse();
	}
}