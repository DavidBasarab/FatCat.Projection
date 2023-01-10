using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.ProjectUsing;

public class NonStronglyTypedCustomProjection
{
	private readonly NonStronglyTypedDestination destination;

	public NonStronglyTypedCustomProjection()
	{
		destination = Faker.Create<NonStronglyTypedDestination>();

		ProjectionConfiguration.UseCustomProjection<TestingCustomProjection>(typeof(NonStronglyTypedDestination));
	}

	[Fact]
	public void WillUseNonStronglyTypedProjection()
	{
		var source = Faker.Create<NonStronglyTypedSource>();

		var result = Projection.ProjectTo(typeof(NonStronglyTypedDestination), source);

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

	private class NonStronglyTypedDestination : MultiLevelObjectDestination { }

	private class NonStronglyTypedSource : MultiLevelObjectSource { }

	private class TestingCustomProjection : IDoProjection
	{
		public static object CalledSource { get; private set; }

		public static NonStronglyTypedDestination ItemToReturn { get; } = Faker.Create<NonStronglyTypedDestination>();

		public static bool WasProjectCalled { get; private set; }

		public static bool WasProjectToCalled { get; private set; }

		public static void Reset()
		{
			WasProjectToCalled = false;
			WasProjectCalled = false;
			CalledSource = null;
		}

		public void Project(object destinationObject, object source)
		{
			destinationObject = ItemToReturn;

			WasProjectCalled = true;
			CalledSource = source;
		}

		public object ProjectTo(object source)
		{
			WasProjectToCalled = true;
			CalledSource = source;

			return ItemToReturn;
		}
	}
}