namespace Domain.Events;

public class ShiftRoleAssignment
{
    public Guid Id { get; }
    public Guid RoleId { get; }
    public int MaxVolunteers { get; }

    public ShiftRoleAssignment(Guid id, Guid roleId, int maxVolunteers)
    {
        if (roleId == Guid.Empty)
            throw new ArgumentException("RoleId cannot be empty.", nameof(roleId));

        if (maxVolunteers <= 0)
            throw new ArgumentException(
                "Number of maximum Volunteers needs to be greater than 0",
                nameof(maxVolunteers)
            );

        Id = id;
        RoleId = roleId;
        MaxVolunteers = maxVolunteers;
    }
}
