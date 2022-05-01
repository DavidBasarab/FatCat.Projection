using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.FluentObjects;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.FluentProjections;

public class ComplexFluentChanges
{
	[Fact]
	public void CanMapWithComplexProperties()
	{
		var source = Faker.Create<FluentComplexSource>();

		var result = new FluentProjection<FluentComplexDestination, FluentComplexSource>()
					.ForProperty(i => i.TheNumber, s => s.Simple.Number)
					.ForProperty(i => i.FullTitle, s => s.Title)
					.Project(source);

		result.FullTitle
			.Should()
			.Be(source.Title);

		result.TheNumber
			.Should()
			.Be(source.Simple.Number);
	}
}