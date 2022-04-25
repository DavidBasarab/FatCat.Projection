using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests;

public class Validation
{
	public static IEnumerable<object[]> InvalidProjections
	{
		get
		{
			yield return new object[]
						{
							typeof(SourceItemsWithStuff),      // Source Type
							typeof(List<SourceItemsWithStuff>) // Destination Type
						};

			yield return new object[]
						{
							typeof(List<SourceItemsWithStuff>), // Source Type
							typeof(SourceItemsWithStuff)        // Destination Type
						};

			yield return new object[]
						{
							typeof(SourceItemsWithStuff), // Source Type
							typeof(int)                   // Destination Type
						};

			yield return new object[]
						{
							typeof(int),                       // Source Type
							typeof(List<SourceItemsWithStuff>) // Destination Type
						};

			yield return new object[]
						{
							typeof(SourceItemsWithStuff), // Source Type
							typeof(string)                // Destination Type
						};

			yield return new object[]
						{
							typeof(string),              // Source Type
							typeof(SourceItemsWithStuff) // Destination Type
						};
		}
	}

	[Theory]
	[MemberData(nameof(InvalidProjections), MemberType = typeof(Validation))]
	public void ThrowInvalidProjectionException(Type sourceType, Type destinationType)
	{
		var source = Faker.Create(sourceType);

		Action invalidAction = () => Projection.ProjectTo(destinationType, source);

		invalidAction
			.Should()
			.Throw<InvalidProjectionException>();
	}
}