using FatCat.Fakes;
using FluentAssertions;
using Xunit;
using TypeExtensions = FatCat.Projections.Extensions.TypeExtensions;

namespace FatCat.Projections.Tests.BasicProjection;

public class ByteArrayProjection
{
	[Fact]
	public void CanCopyTheArray()
	{
		var source = Faker.Create<ArraySource>();

		var sourceArray = (Array)source.AByteArray;

		var otherArray = Array.CreateInstance(source.AByteArray.GetType().GetElementType(), source.AByteArray.Length);

		// var otherArray = Activator.CreateInstance(source.AByteArray.GetType().GetElementType());

		Array.Copy(sourceArray, otherArray, sourceArray.Length);

		otherArray.Should()
				.BeEquivalentTo(source.AByteArray);
	}

	[Fact]
	public void CanProjectAnObjectWithByteArray()
	{
		var source = Faker.Create<ArraySource>();

		var result = Projection.ProjectTo<ArrayDestination>(source);

		result
			.Should()
			.BeEquivalentTo(source);
	}

	[Fact]
	public void TestingIsListForByteArray()
	{
		var source = Faker.Create<ArraySource>();

		TypeExtensions.IsArray(source.AByteArray.GetType())
					.Should()
					.BeTrue();
	}
}

public class ArraySource
{
	public byte[] AByteArray { get; set; }
}

public class ArrayDestination
{
	public byte[] AByteArray { get; set; }
}