using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.FluentObjects;

namespace FatCat.Projections.Tests.FluentProjections;

public class ComplexFluentChanges
{
    [Fact]
    public void CanMapWithComplexFluentObjects()
    {
        var source = Faker.Create<FluentComplexSource>();

        var result = new FluentProjection<FluentComplexDestination, FluentComplexSource>()
            .ForProperty(i => i.SubObject.FullTitle, s => s.Title)
            .ForProperty(i => i.SubObject.TheNumber, s => s.Simple.Number)
            .Project(source);

        result.SubObject.Should().NotBeNull();

        result.SubObject.FullTitle.Should().Be(source.Title);

        result.SubObject.TheNumber.Should().Be(source.Simple.Number);
    }

    [Fact]
    public void CanMapWithComplexProperties()
    {
        var source = Faker.Create<FluentComplexSource>();

        var result = new FluentProjection<FluentKindOfSimpleDestination, FluentComplexSource>()
            .ForProperty(i => i.TheNumber, s => s.Simple.Number)
            .ForProperty(i => i.FullTitle, s => s.Title)
            .Project(source);

        result.FullTitle.Should().Be(source.Title);

        result.TheNumber.Should().Be(source.Simple.Number);
    }
}
