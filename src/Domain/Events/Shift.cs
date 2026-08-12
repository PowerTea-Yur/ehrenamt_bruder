namespace Domain.Events;

public class Shift
{
    private readonly List<ShiftRoleAssignment> _roleAssignments = new();

    public Guid Id { get; }
    public DateOnly ShiftDate { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public bool IsCancelled { get; private set; } = false;
    public IReadOnlyCollection<ShiftRoleAssignment> RoleAssignments =>
        _roleAssignments.AsReadOnly();

    public Shift(Guid id, DateOnly shiftDate, TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
            throw new ArgumentException(
                "StartTime can't be larger than EndTime",
                nameof(startTime)
            );

        Id = id;
        ShiftDate = shiftDate;
        StartTime = startTime;
        EndTime = endTime;
    }

    internal void AddRoleAssignment(ShiftRoleAssignment assignment)
    {
        _roleAssignments.Add(assignment);
    }

    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("The Shift is already canceled");

        IsCancelled = true;
    }
}
