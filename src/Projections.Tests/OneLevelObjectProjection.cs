using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.OneLevelComplexItems;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests
{
	public class OneLevelObjectProjection
	{
		[Fact]
		public void CanProjectOnSubObject()
		{
			var source = Faker.Create<OneLevelSource>();

			var result = Projection.ProjectTo<OneLevelDestination>(source);

			ReferenceEquals(source.SubObject, result.SubObject)
				.Should()
				.BeFalse();

			result.Should().BeEquivalentTo(source);
		}
	}
}