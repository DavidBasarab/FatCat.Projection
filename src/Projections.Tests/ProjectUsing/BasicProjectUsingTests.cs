using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.ProjectUsing;

public class BasicProjectUsingTests
{
	public BasicProjectUsingTests() => ProjectionConfiguration.UseCustomProjection<MultiLevelObjectDestination, TestingCustomProjection>();

	[Fact]
	public void WillUseCustomProjectionForProjectTo()
	{
		var source = Faker.Create<MultiLevelObjectSource>();

		var result = Projection.ProjectTo<MultiLevelObjectDestination>(source);

		result
			.Should()
			.Be(TestingCustomProjection.ItemToReturn);

		TestingCustomProjection.WasProjectToCalled
								.Should()
								.BeTrue();

		TestingCustomProjection.CalledSource
								.Should()
								.Be(source);
	}

	private class TestingCustomProjection : IDoProjection<MultiLevelObjectDestination>
	{
		public static object CalledSource { get; private set; }

		public static MultiLevelObjectDestination ItemToReturn { get; } = Faker.Create<MultiLevelObjectDestination>();

		public static bool WasProjectCalled { get; private set; }

		public static bool WasProjectToCalled { get; private set; }

		public static void Reset()
		{
			WasProjectToCalled = false;
			WasProjectCalled = false;
			CalledSource = null;
		}

		public void Project(MultiLevelObjectDestination destinationObject, object source)
		{
			destinationObject = ItemToReturn;

			WasProjectCalled = true;
			CalledSource = source;
		}

		public MultiLevelObjectDestination ProjectTo(object source)
		{
			WasProjectToCalled = true;
			CalledSource = source;

			return ItemToReturn;
		}
	}
}