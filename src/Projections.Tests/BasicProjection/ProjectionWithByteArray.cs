using FatCat.Fakes;

namespace FatCat.Projections.Tests.BasicProjection;

public class ProjectionWithByteArray
{
    [Fact]
    public void CanProjectAnObjectWithBytes()
    {
        var source = Faker.Create<SourceClass>();

        var destination = Projection.ProjectTo<DestinationClass>(source);

        destination.Email.Should().Be(source.Email);
        destination.FirstName.Should().Be(source.FirstName);
        destination.LastName.Should().Be(source.LastName);
        destination.PhoneNumber.Should().Be(source.PhoneNumber);
        destination.Username.Should().Be(source.Username);
    }

    public class DestinationClass
    {
        public DateTime CreatedDate { get; set; }

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string PublicKey { get; set; } = null!;

        public byte[] UserKey { get; set; } = null!;

        public string Username { get; set; } = null!;

        public byte[] UserSignature { get; set; } = null!;
    }

    public class SourceClass
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }
    }
}
