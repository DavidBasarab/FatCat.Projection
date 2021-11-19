using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.SimpleItems;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests
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

		[Fact]
		public void ExtraPropertiesAreNotPopulated()
		{
			var source = Faker.Create<SimpleSource>();

			var result = Projection.ProjectTo<SimpleDestinationMoreProperties>(source);

			result.LastName.Should().BeNullOrEmpty();
			result.FirstName.Should().Be(source.FirstName);
			result.Number.Should().Be(source.Number);
		}

		[Fact]
		public void IfPropertyIsNotOnDestinationDoesNotError()
		{
			var source = Faker.Create<SimpleSource>();

			var result = Projection.ProjectTo<SimpleDestinationMissingProperty>(source);

			result.Number.Should().Be(source.Number);
		}

		[Fact]
		public void ItemsWithListCanBeProjected()
		{
			var source = Faker.Create<SourceItemWithList>();

			var result = Projection.ProjectTo<DestinationItemWithList>(source);

			result.Should().BeEquivalentTo(source);
		}
	}
}