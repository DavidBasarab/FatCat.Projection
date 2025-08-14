using FatCat.Fakes;
using FatCat.Toolkit.Extensions;

namespace FatCat.Projections.Tests.BasicProjection;

public class DoNotProjectNullAsOption
{
    [Fact]
    public void CanTurnOnOptionToNotProjectNull()
    {
        var source = new SourceItem { FirstName = "Will be populated" };

        var destinationItem = Faker.Create<DestinationItem>();
        var projectionItem = (object)destinationItem.DeepCopy();

        Projection.ProjectTo(ref projectionItem, source, ProjectionSettings.DoNotProjectNull);

        VerifyResult(projectionItem, source, destinationItem);
    }

    [Fact]
    public void DoNotNullSubObjectOnProjection()
    {
        var source = new SourceItem { FirstName = "Will be populated" };

        var destinationItem = Faker.Create<DestinationItem>();
        var projectionItem = (object)destinationItem.DeepCopy();

        var projector = new Projector();

        projector.ProjectTo(ref projectionItem, source, ProjectionSettings.DoNotProjectNull);

        var result = (DestinationItem)projectionItem;

        result.SubObject.Should().NotBeNull();

        result.SubObject.Should().BeEquivalentTo(destinationItem.SubObject);
    }

    [Fact]
    public void DoNotProjectNullUsingNonStaticProjector()
    {
        var source = new SourceItem { FirstName = "Will be populated" };

        var destinationItem = Faker.Create<DestinationItem>();
        var projectionItem = (object)destinationItem.DeepCopy();

        var projector = new Projector();

        projector.ProjectTo(ref projectionItem, source, ProjectionSettings.DoNotProjectNull);

        VerifyResult(projectionItem, source, destinationItem);
    }

    [Fact]
    public void DoProjectionAsAWebRequest()
    {
        var source = new SimulateWebRequest { FirstName = "Will be populated" };

        var destinationItem = Faker.Create<DestinationItem>();
        var projectionItem = (object)destinationItem.DeepCopy();

        Projection.ProjectTo(ref projectionItem, source, ProjectionSettings.DoNotProjectNull);

        var result = (DestinationItem)projectionItem;

        result.FirstName.Should().Be(source.FirstName);

        result.SecondName.Should().Be(destinationItem.SecondName);

        result.ThirdName.Should().Be(destinationItem.ThirdName);
    }

    private static void VerifyResult(
        object projectionItem,
        SourceItem source,
        DestinationItem destinationItem
    )
    {
        var result = (DestinationItem)projectionItem;

        result.FirstName.Should().Be(source.FirstName);

        result.SecondName.Should().Be(destinationItem.SecondName);

        result.ThirdName.Should().Be(destinationItem.ThirdName);
    }

    public class DestinationItem : SourceItem { }

    public class SimulateWebRequest
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }
    }

    public class SourceItem
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public SubObject SubObject { get; set; }

        public string ThirdName { get; set; }
    }

    public class SubObject
    {
        public int FirstNumber { get; set; }

        public string SecondName { get; set; }

        public string ThirdThing { get; set; }
    }
}
