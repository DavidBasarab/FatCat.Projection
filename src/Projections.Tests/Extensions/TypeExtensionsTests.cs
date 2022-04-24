using FatCat.Projections.Extensions;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.Extensions;

public class TypeExtensionsTests
{
	[Theory]
	[InlineData(typeof(List<SourceItemsWithStuff>))]
	public void IsList(Type typeToCheck)
	{
		typeToCheck.IsList()
					.Should()
					.BeTrue();
	}
}