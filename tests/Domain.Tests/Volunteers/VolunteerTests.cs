using Domain.Volunteers;

namespace Domain.Tests.Volunteers;

public class VolunteerTests
{
    private static Volunteer CreateVolunteer(string name = "Jane Doe")
    {
        return new Volunteer(Guid.NewGuid(), name);
    }

    [Fact]
    public void Constructor_WithValidName_CreatesVolunteer()
    {
        var sut = CreateVolunteer();

        Assert.Equal("Jane Doe", sut.Name);
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CreateVolunteer(name: ""));
    }
}
