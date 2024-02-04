using FatCat.Fakes;

namespace FatCat.Projections.Tests.BasicProjection;

public class ProjectionWithMissingProperty
{
    [Fact]
    public void CanProjectWithMissingDestinationFromSource()
    {
        var source = Faker.Create<Source>();

        var destination = Projection.ProjectTo<Destination>(source);
    }

    private class Destination
    {
        public string FirstName { get; set; }

        public string Id { get; set; }

        public string LastName { get; set; }

        public SomeStruct Some { get; set; }
    }

    private struct SomeStruct
    {
        public int a;
        public int b;

        public SomeStruct()
        {
            a = 0;
            b = 0;
        }
    }

    private class Source
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
