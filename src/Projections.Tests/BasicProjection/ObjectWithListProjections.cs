using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;

namespace FatCat.Projections.Tests.BasicProjection;

public class ObjectWithListProjections
{
    [Fact]
    public void CanProjectWithBasicListItems()
    {
        var source = Faker.Create<SourceItemWithList>();

        var result = Projection.ProjectTo<DestinationItemWithList>(source);

        CompareLists(source.Numbers, result.Numbers);
    }

    [Fact]
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

        source.Should().BeEquivalentTo(result);
    }

    private static void CompareLists<T>(IEnumerable<T> sourceList, IEnumerable<T> resultList)
    {
        sourceList.Should().BeEquivalentTo(resultList);

        ReferenceEquals(sourceList, resultList).Should().BeFalse();
    }
}
