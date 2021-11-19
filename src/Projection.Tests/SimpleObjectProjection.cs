using FatCat.Fakes;
using FatCat.Tests.Objects;
using FluentAssertions;
using Xunit;

namespace FatCat.Tests
{
	public class SimpleObjectProjection
	{
		[Fact]
		public void CanProjectFromSimpleDestinationToSource()
		{
			var source = Faker.Create<SimpleSource>();

			var result = Projection.ProjectTo<SimpleDestination>(source);

			result.Should().BeOfType<SimpleDestination>();

			result.Should().BeEquivalentTo(source);
		}
	}
}