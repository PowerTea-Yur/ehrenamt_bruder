using Domain.Organizations;

namespace Domain.Tests.Organizations;

public class OrganizationTests
{
    [Fact]
    public void Constructor_WithValidName_CreatesOrganization()
    {
        var sut = new Organization(Guid.NewGuid(), "Red Cross Frankfurt");

        Assert.Equal("Red Cross Frankfurt", sut.Name);
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Organization(Guid.NewGuid(), ""));
    }
}
