using Domain.Events;

namespace Domain.Tests.Events;

public class ShiftRoleAssignmentTests
{
    private static ShiftRoleAssignment CreateAssignment(
        ShiftRole? role = null,
        int maxVolunteers = 2
    )
    {
        return new ShiftRoleAssignment(
            Guid.NewGuid(),
            role ?? new ShiftRole(Guid.NewGuid(), "Boat Driver", true),
            maxVolunteers
        );
    }

    [Fact]
    public void Constructor_WithValidArguments_CreatesAssignment()
    {
        var roleId = Guid.NewGuid();
        var role = new ShiftRole(roleId, "Boat Driver", true);

        var sut = CreateAssignment(role: role, maxVolunteers: 3);

        Assert.Equal(roleId, sut.Role.Id);
        Assert.Equal(3, sut.MaxVolunteers);
    }

    [Fact]
    public void Constructor_WithNullRole_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new ShiftRoleAssignment(Guid.NewGuid(), null!, maxVolunteers: 2)
        );
    }

    [Fact]
    public void Constructor_WithNonPositiveMaxVolunteers_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => CreateAssignment(maxVolunteers: 0));
    }
}
