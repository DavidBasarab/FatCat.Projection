using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.FluentObjects;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.FluentProjections;

public class BasicFluentChanges
{
    [Fact]
    public void CanDoABasicOverrideMap()
    {
        var source = Faker.Create<FluentSimpleSource>();

        var result = new FluentProjection<FluentSimpleDestination, FluentSimpleSource>()
            .ForProperty(i => i.DifferentNumber, _ => 23)
            .Project(source);

        result.Name.Should().Be(source.Name);

        result.DifferentNumber.Should().Be(23);
    }

    [Fact]
    public void CanDoOverrideFromObjectProperties()
    {
        var source = Faker.Create<FluentSimpleSource>();

        var result = new FluentProjection<FluentSimpleDestination, FluentSimpleSource>()
            .ForProperty(i => i.DifferentNumber, _ => source.Number)
            .Project(source);

        result.Name.Should().Be(source.Name);

        result.DifferentNumber.Should().Be(source.Number);
    }

    [Fact]
    public void CanSetThePropertyAsNull()
    {
        var source = Faker.Create<FluentSimpleSource>();

        var result = new FluentProjection<FluentSimpleDestination, FluentSimpleSource>()
            .ForProperty(i => i.Name, _ => null)
            .Project(source);

        result.Name.Should().BeNull();
    }
}
