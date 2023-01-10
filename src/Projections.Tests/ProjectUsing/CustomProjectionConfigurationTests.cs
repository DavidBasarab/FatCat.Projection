using FatCat.Projections.Tests.Objects.SimpleItems;
using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.ProjectUsing;

public class CustomProjectionConfigurationTests
{
	[Fact]
	public void CanSaveACustomProjectorToConfig()
	{
		ProjectionConfiguration.UseCustomProjection<SimpleDestination, TestProjection>();

		RunGetCustomerProjectorTest<TestProjection>();
	}

	[Fact]
	public void TheLastCustomProjectorWillBeUsed()
	{
		ProjectionConfiguration.UseCustomProjection<SimpleDestination, TestProjection>();
		ProjectionConfiguration.UseCustomProjection<SimpleDestination, SecondCustomProjection>();

		RunGetCustomerProjectorTest<SecondCustomProjection>();
	}

	private static void RunGetCustomerProjectorTest<T>() where T : IDoProjection<SimpleDestination>
	{
		var customProjector = ProjectionConfiguration.GetCustomProjector<SimpleDestination>();

		customProjector
			.Should()
			.NotBeNull();

		customProjector
			.Should()
			.BeOfType<T>();
	}

	private class SecondCustomProjection : IDoProjection<SimpleDestination>
	{
		public void Project(SimpleDestination destinationObject, object source) { throw new NotImplementedException(); }

		public SimpleDestination ProjectTo(object source) => throw new NotImplementedException();
	}

	private class TestProjection : IDoProjection<SimpleDestination>
	{
		public void Project(SimpleDestination destinationObject, object source) { throw new NotImplementedException(); }

		public SimpleDestination ProjectTo(object source) => throw new NotImplementedException();
	}
}