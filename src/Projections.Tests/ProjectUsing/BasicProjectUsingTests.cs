using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.ProjectUsing;

public class BasicProjectUsingTests
{
	public BasicProjectUsingTests() => ProjectionConfiguration.UseCustomProjection<CustomProjectionDestination, TestingCustomProjection>();

	[Fact]
	public void WillUseCustomProjectionForProjectTo()
	{
		var source = Faker.Create<CustomProjectionSource>();

		var result = Projection.ProjectTo<CustomProjectionDestination>(source);

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

	private class CustomProjectionDestination : MultiLevelObjectDestination { }

	public class CustomProjectionSource : MultiLevelObjectSource { }

	private class TestingCustomProjection : IDoProjection<CustomProjectionDestination>
	{
		public static object CalledSource { get; private set; }

		public static CustomProjectionDestination ItemToReturn { get; } = Faker.Create<CustomProjectionDestination>();

		public static bool WasProjectCalled { get; private set; }

		public static bool WasProjectToCalled { get; private set; }

		public static void Reset()
		{
			WasProjectToCalled = false;
			WasProjectCalled = false;
			CalledSource = null;
		}

		public void Project(CustomProjectionDestination destinationObject, object source)
		{
			destinationObject = ItemToReturn;

			WasProjectCalled = true;
			CalledSource = source;
		}

		public CustomProjectionDestination ProjectTo(object source)
		{
			WasProjectToCalled = true;
			CalledSource = source;

			return ItemToReturn;
		}
	}
}