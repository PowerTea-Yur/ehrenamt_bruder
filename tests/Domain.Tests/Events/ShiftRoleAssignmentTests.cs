using Domain.Events;

namespace Domain.Tests.Events;

public class ShiftRoleAssignmentTests
{
    private static ShiftRoleAssignment CreateAssignment(Guid? roleId = null, int maxVolunteers = 2)
    {
        return new ShiftRoleAssignment(Guid.NewGuid(), roleId ?? Guid.NewGuid(), maxVolunteers);
    }

    [Fact]
    public void Constructor_WithValidArguments_CreatesAssignment()
    {
        var roleId = Guid.NewGuid();

        var sut = CreateAssignment(roleId: roleId, maxVolunteers: 3);

        Assert.Equal(roleId, sut.RoleId);
        Assert.Equal(3, sut.MaxVolunteers);
    }

    [Fact]
    public void Constructor_WithEmptyRoleId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CreateAssignment(roleId: Guid.Empty));
    }

    [Fact]
    public void Constructor_WithNonPositiveMaxVolunteers_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CreateAssignment(maxVolunteers: 0));
    }
}
