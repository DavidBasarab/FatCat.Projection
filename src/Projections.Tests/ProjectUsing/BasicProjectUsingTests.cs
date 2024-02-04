using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;

namespace FatCat.Projections.Tests.ProjectUsing;

public class BasicProjectUsingTests
{
    public BasicProjectUsingTests()
    {
        ProjectionConfiguration.UseCustomProjection<CustomProjectionDestination, TestingCustomProjection>();
    }

    [Fact]
    public void UseCustomProjectionUsingProjectorInstance()
    {
        TestingCustomProjection.Reset();

        var projector = new Projector();

        var source = Faker.Create<CustomProjectionSource>();

        var result = projector.ProjectTo<CustomProjectionDestination>(source);

        VerifyProjectionResult(result, source);
    }

    [Fact]
    public void WhenProjectingToAListWillUseCustomProjection()
    {
        TestingCustomProjection.Reset();

        var source = Faker.Create<List<CustomProjectionSource>>(2);

        var result = Projection.ProjectTo<List<CustomProjectionDestination>>(source);

        TestingCustomProjection.WasProjectToCalled.Should().BeTrue();

        var expectedList = new List<CustomProjectionDestination>
        {
            TestingCustomProjection.ItemToReturn,
            TestingCustomProjection.ItemToReturn
        };

        result.Should().BeEquivalentTo(expectedList);
    }

    [Fact]
    public void WillUseCustomProjectionForProjectTo()
    {
        TestingCustomProjection.Reset();

        var source = Faker.Create<CustomProjectionSource>();

        var result = Projection.ProjectTo<CustomProjectionDestination>(source);

        VerifyProjectionResult(result, source);
    }

    private static void VerifyProjectionResult(
        CustomProjectionDestination result,
        CustomProjectionSource source
    )
    {
        result.Should().Be(TestingCustomProjection.ItemToReturn);

        TestingCustomProjection.WasProjectToCalled.Should().BeTrue();

        TestingCustomProjection.CalledSource.Should().Be(source);
    }

    private class CustomProjectionDestination : MultiLevelObjectDestination { }

    public class CustomProjectionSource : MultiLevelObjectSource { }

    private class TestingCustomProjection : IDoProjection<CustomProjectionDestination>
    {
        public static object CalledSource { get; private set; }

        public static CustomProjectionDestination ItemToReturn { get; } =
            Faker.Create<CustomProjectionDestination>();

        public static bool WasProjectCalled { get; private set; }

        public static bool WasProjectToCalled { get; private set; }

        public static bool WasProjectToObjectCalled { get; private set; }

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

        public void Project(ref object destinationObject, object source)
        {
            throw new NotImplementedException();
        }

        public CustomProjectionDestination ProjectTo(object source)
        {
            WasProjectToCalled = true;
            CalledSource = source;

            return ItemToReturn;
        }

        public object ProjectToObject(object source)
        {
            WasProjectToObjectCalled = true;

            WasProjectToCalled = true;
            CalledSource = source;

            return ItemToReturn;
        }
    }
}
