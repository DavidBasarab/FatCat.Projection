using FluentAssertions;
using Xunit;

namespace FatCat.Projections.Tests.ProjectUsing;

public class CustomProjectionConfigurationTests
{
	[Fact]
	public void CanConfigureANonStronglyTypeProjectionConfiguration()
	{
		ProjectionConfiguration.UseCustomProjection<NonStronglyTypedProjection>(typeof(NonStronglyTypeDestination));

		var projector = ProjectionConfiguration.GetCustomProjector(typeof(NonStronglyTypeDestination));

		projector
			.Should()
			.NotBeNull();

		projector
			.Should()
			.BeOfType<NonStronglyTypedProjection>();
	}

	[Fact]
	public void CanSaveACustomProjectorToConfig()
	{
		ProjectionConfiguration.UseCustomProjection<ConfigTestDestination, TestProjection>();

		RunGetCustomerProjectorTest<TestProjection>();
	}

	[Fact]
	public void IfTypeIsNotConfiguredToBeOverriddenANullIsReturned()
	{
		ProjectionConfiguration.GetCustomProjector<NeverGetConfiguredDestination>()
								.Should()
								.BeNull();
	}

	[Fact]
	public void TheLastCustomProjectorWillBeUsed()
	{
		ProjectionConfiguration.UseCustomProjection<ConfigTestDestination, TestProjection>();
		ProjectionConfiguration.UseCustomProjection<ConfigTestDestination, SecondCustomProjection>();

		RunGetCustomerProjectorTest<SecondCustomProjection>();
	}

	private static void RunGetCustomerProjectorTest<T>() where T : IDoProjection<ConfigTestDestination>
	{
		var customProjector = ProjectionConfiguration.GetCustomProjector<ConfigTestDestination>();

		customProjector
			.Should()
			.NotBeNull();

		customProjector
			.Should()
			.BeOfType<T>();
	}

	private class ConfigTestDestination { }

	private class NeverGetConfiguredDestination { }

	private class NonStronglyTypeDestination { }

	private class NonStronglyTypedProjection : IDoProjection
	{
		public void Project(ref object destinationObject, object source) {  }

		public object ProjectToObject(object source) => throw new NotImplementedException();
	}

	private class SecondCustomProjection : IDoProjection<ConfigTestDestination>
	{
		public void Project(ConfigTestDestination destinationObject, object source) { throw new NotImplementedException(); }

		public ConfigTestDestination ProjectTo(object source) => throw new NotImplementedException();

		public void Project(ref object destinationObject, object source) { throw new NotImplementedException(); }

		public object ProjectToObject(object source) => throw new NotImplementedException();
	}

	private class TestProjection : IDoProjection<ConfigTestDestination>
	{
		public void Project(ConfigTestDestination destinationObject, object source) { throw new NotImplementedException(); }

		public ConfigTestDestination ProjectTo(object source) => throw new NotImplementedException();

		public void Project(ref object destinationObject, object source) { throw new NotImplementedException(); }

		public object ProjectToObject(object source) => throw new NotImplementedException();
	}
}