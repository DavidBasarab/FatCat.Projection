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

		result.Name
			.Should()
			.Be(source.Name);

		result.DifferentNumber
			.Should()
			.Be(23);
	}
}