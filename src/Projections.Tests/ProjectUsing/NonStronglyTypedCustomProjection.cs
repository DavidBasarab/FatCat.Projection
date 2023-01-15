using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.ProjectUsing;

public class NonStronglyTypedCustomProjection
{
	public NonStronglyTypedCustomProjection() => ProjectionConfiguration.UseCustomProjection<TestingCustomProjection>(typeof(NonStronglyTypedDestination));

	[Fact]
	public void OnProjectionObjectWillUseNonStronglyTypedProjection()
	{
		var source = Faker.Create<NonStronglyTypedSource>();

		var projector = new Projector();

		var result = projector.ProjectTo(typeof(NonStronglyTypedDestination), source);

		VerifyProjectTo(result, source);
	}

	[Fact]
	public void OnProjectionUsingExistingObjectWillUseNonStronglyTypedProjection()
	{
		var source = Faker.Create<NonStronglyTypedSource>();
		object destination = new NonStronglyTypedDestination();

		var projector = new Projector();

		projector.ProjectTo(ref destination, source);

		VerifyProjectByReference(destination, source);
	}

	[Fact]
	public void WillUseNonStronglyTypedProjectionOnExistingObject()
	{
		var source = Faker.Create<NonStronglyTypedSource>();
		object destination = new NonStronglyTypedDestination();

		Projection.ProjectTo(ref destination, source);

		VerifyProjectByReference(destination, source);
	}

	[Fact]
	public void WillUseNonStronglyTypedProjection()
	{
		var source = Faker.Create<NonStronglyTypedSource>();

		var result = Projection.ProjectTo(typeof(NonStronglyTypedDestination), source);

		VerifyProjectTo(result, source);
	}

	private static void VerifyProjectTo(object result, NonStronglyTypedSource source)
	{
		result
			.Should()
			.BeEquivalentTo(TestingCustomProjection.ItemToReturn);

		TestingCustomProjection.WasProjectToCalled
								.Should()
								.BeTrue();

		TestingCustomProjection.CalledSource
								.Should()
								.Be(source);
	}
	
	private static void VerifyProjectByReference(object result, NonStronglyTypedSource source)
	{
		result
			.Should()
			.BeEquivalentTo(TestingCustomProjection.ItemToReturn);

		TestingCustomProjection.WasProjectCalled
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

		public void Project(ref object destinationObject, object source)
		{
			destinationObject = ItemToReturn;

			WasProjectCalled = true;
			CalledSource = source;
		}

		public object ProjectToObject(object source)
		{
			WasProjectToCalled = true;
			CalledSource = source;

			return ItemToReturn;
		}
	}
}