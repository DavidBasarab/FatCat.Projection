using FatCat.Fakes;

namespace FatCat.Projections.Tests.BasicProjection;

public class ProjectionWithNullProperty
{
    [Fact]
    public void CanProjectWithANullSubObject()
    {
        var source = Faker.Create<SourceObject>();

        source.Sub = null;

        var result = Projection.ProjectTo<SourceObject>(source);

        result.Sub.Should().BeNull();
    }

    public class SourceObject
    {
        public SubObject Sub { get; set; }
    }

    public class SubObject
    {
        public string Password { get; set; }

        public string Username { get; set; }
    }
}
