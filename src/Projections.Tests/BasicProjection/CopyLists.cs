using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.OneLevelComplexItems;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;

namespace FatCat.Projections.Tests.BasicProjection;

public class CopyLists
{
    [Fact]
    public void CanCopyAListToAnotherUniqueList()
    {
        var sourceList = Faker.Create<List<SubObject>>();

        var copyList = ListCopy.Copy(sourceList);

        copyList.Should().NotBeSameAs(sourceList);

        var strongList = copyList as List<SubObject>;

        strongList.Should().BeOfType<List<SubObject>>();

        strongList.Should().BeEquivalentTo(sourceList);
    }

    [Fact]
    public void CanCopyListToADifferentListType()
    {
        var sourceList = Faker.Create<List<SourceItemsWithStuff>>();

        var copyList = ListCopy.Copy(sourceList, typeof(DestinationItemsWithStuff));

        copyList.Should().NotBeSameAs(sourceList);

        var strongList = copyList as List<DestinationItemsWithStuff>;

        strongList.Should().BeOfType<List<DestinationItemsWithStuff>>();

        strongList.Should().BeEquivalentTo(sourceList);
    }
}
