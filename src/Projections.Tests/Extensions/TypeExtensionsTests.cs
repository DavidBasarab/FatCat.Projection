using FatCat.Projections.Extensions;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.Extensions;

public class TypeExtensionsTests
{
	[Theory]
	[InlineData(typeof(int), true)]
	[InlineData(typeof(short), true)]
	[InlineData(typeof(float), true)]
	[InlineData(typeof(float?), true)]
	[InlineData(typeof(DateTime), true)]
	[InlineData(typeof(TimeSpan), true)]
	[InlineData(typeof(string), true)]
	[InlineData(typeof(SourceItemsWithStuff), false)]
	[InlineData(typeof(List<SourceItemsWithStuff>), false)]
	public void IsBasicType(Type typeToTest, bool isBasic)
	{
		typeToTest
			.IsBasicType()
			.Should()
			.Be(isBasic);
	}

	[Theory]
	[InlineData(typeof(List<SourceItemsWithStuff>))]
	public void IsList(Type typeToCheck)
	{
		typeToCheck.IsList()
					.Should()
					.BeTrue();
	}
}