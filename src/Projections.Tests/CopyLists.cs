using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.OneLevelComplexItems;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests;

public class CopyLists
{
	[Fact]
	public void CanCopyAListToAnotherUniqueList()
	{
		var sourceList = Faker.Create<List<SubObject>>();

		var copyList = ListCopy.Copy(sourceList);

		copyList
			.Should()
			.NotBeSameAs(sourceList);

		var strongList = copyList as List<SubObject>;

		strongList
			.Should()
			.BeEquivalentTo(sourceList);
	}
}