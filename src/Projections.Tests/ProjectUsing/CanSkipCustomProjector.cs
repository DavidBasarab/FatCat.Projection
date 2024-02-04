using FatCat.Fakes;
using FatCat.Toolkit;

namespace FatCat.Projections.Tests.ProjectUsing;

public class CanSkipCustomProjector
{
    private readonly Projector projector = new();

    public CanSkipCustomProjector()
    {
        ProjectionConfiguration.UseCustomProjection<AnExampleCustomProjector>(typeof(AnExampleDestination));
    }

    [Fact]
    public void WillSkipCustomProjectorForCreatedObject()
    {
        var source = Faker.Create<AnExampleSource>();
        object destination = new AnExampleDestination();

        projector.ProjectTo(ref destination, source, ProjectionSettings.SkipCustomProjector);

        var expectedDestination = new AnExampleDestination
        {
            RandomNumber = source.RandomNumber,
            RandomString = source.RandomString,
            SecondRandomString = source.SecondRandomString
        };

        destination.Should().BeEquivalentTo(expectedDestination);
    }

    [Fact]
    public void WillSkipCustomProjectorForGeneric()
    {
        var source = Faker.Create<AnExampleSource>();

        var destination = projector.ProjectTo<AnExampleDestination>(
            source,
            ProjectionSettings.SkipCustomProjector
        );

        var expectedDestination = new AnExampleDestination
        {
            RandomNumber = source.RandomNumber,
            RandomString = source.RandomString,
            SecondRandomString = source.SecondRandomString
        };

        destination.Should().BeEquivalentTo(expectedDestination);
    }

    [Fact]
    public void WillSkipCustomProjectorForType()
    {
        var source = Faker.Create<AnExampleSource>();

        var destination = projector.ProjectTo(
            typeof(AnExampleDestination),
            source,
            ProjectionSettings.SkipCustomProjector
        );

        var expectedDestination = new AnExampleDestination
        {
            RandomNumber = source.RandomNumber,
            RandomString = source.RandomString,
            SecondRandomString = source.SecondRandomString
        };

        destination.Should().BeEquivalentTo(expectedDestination);
    }

    [Fact]
    public void WillUseCustomProjectorForCreatedObject()
    {
        var source = Faker.Create<AnExampleSource>();
        object destination = new AnExampleDestination();

        projector.ProjectTo(ref destination, source);

        destination.Should().BeEquivalentTo(AnExampleCustomProjector.Destination);
    }

    [Fact]
    public void WillUseCustomProjectorForGeneric()
    {
        var source = Faker.Create<AnExampleSource>();

        var destination = projector.ProjectTo<AnExampleDestination>(source);

        destination.Should().BeEquivalentTo(AnExampleCustomProjector.Destination);
    }

    [Fact]
    public void WillUseCustomProjectorForType()
    {
        var source = Faker.Create<AnExampleSource>();

        var destination = projector.ProjectTo(typeof(AnExampleDestination), source);

        destination.Should().BeEquivalentTo(AnExampleCustomProjector.Destination);
    }

    public class AnExampleCustomProjector : IDoProjection<AnExampleDestination>
    {
        public static AnExampleDestination Destination { get; set; } = Faker.Create<AnExampleDestination>();

        public void Project(ref object destinationObject, object source)
        {
            destinationObject = Destination;
        }

        public AnExampleDestination ProjectTo(object source)
        {
            return Destination;
        }

        public object ProjectToObject(object source)
        {
            return Destination;
        }
    }

    public class AnExampleDestination : EqualObject
    {
        public int RandomNumber { get; set; }

        public string RandomString { get; set; }

        public string SecondRandomString { get; set; }
    }

    public class AnExampleSource : EqualObject
    {
        public int RandomNumber { get; set; }

        public string RandomString { get; set; }

        public string SecondRandomString { get; set; }
    }
}
