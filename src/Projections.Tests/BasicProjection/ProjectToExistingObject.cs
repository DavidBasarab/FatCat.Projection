using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;

namespace FatCat.Projections.Tests.BasicProjection;

public class ProjectToExistingObject
{
    [Fact]
    public void CanGiveAnInstanceAndProjectToIt()
    {
        var sourceItem = Faker.Create<MultiLevelObjectSource>();

        object projectInstance = new MultiLevelObjectSource();

        Projection.ProjectTo(ref projectInstance, sourceItem);

        projectInstance.Should().BeEquivalentTo(sourceItem);
    }
}
