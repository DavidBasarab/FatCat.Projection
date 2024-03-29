using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.OneLevelComplexItems;

namespace FatCat.Projections.Tests.BasicProjection;

public class OneLevelObjectProjection
{
    [Fact]
    public void CanProjectOnSubObject()
    {
        var source = Faker.Create<OneLevelSource>();

        var result = Projection.ProjectTo<OneLevelDestination>(source);

        ReferenceEquals(source.SubObject, result.SubObject).Should().BeFalse();

        result.SubObject.Should().BeEquivalentTo(source.SubObject);

        result.DifferentSubObject.Number.Should().Be(source.DifferentSubObject.Number);
        result.DifferentSubObject.UpdatedDate.Should().Be(source.DifferentSubObject.UpdatedDate);
    }
}
