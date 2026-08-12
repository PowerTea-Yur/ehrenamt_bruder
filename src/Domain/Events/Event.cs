namespace Domain.Events;

public class Event
{
    private readonly List<Shift> _shifts = new();
    private readonly List<ShiftRole> _shiftRoles = new();

    public Guid Id { get; }
    public string Name { get; }
    public Location Location { get; private set; }
    public EventStatus Status { get; private set; } = EventStatus.Draft;
    public IReadOnlyCollection<Shift> Shifts => _shifts.AsReadOnly();
    public IReadOnlyCollection<ShiftRole> ShiftRoles => _shiftRoles.AsReadOnly();

    public Event(Guid id, string name, Location location)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        Id = id;
        Name = name;
        Location = location;
    }

    public void Publish()
    {
        if (Status == EventStatus.Published)
            throw new InvalidOperationException("The Event already has the Published status");

        Status = EventStatus.Published;
    }

    public void AddShift(Shift shift)
    {
        _shifts.Add(shift);
    }

    public void AddShiftRole(ShiftRole role)
    {
        _shiftRoles.Add(role);
    }

    public void AssignRoleToShift(Guid shiftId, Guid roleId, int maxVolunteers)
    {
        var role = _shiftRoles.FirstOrDefault(r => r.Id == roleId);
        if (role is null)
            throw new ArgumentException("This ShiftRole does not belong to this Event.");

        var shift = _shifts.FirstOrDefault(s => s.Id == shiftId);
        if (shift is null)
            throw new ArgumentException("This Shift does not belong to this Event");

        var assignment = new ShiftRoleAssignment(shiftId, roleId, maxVolunteers);
        shift.AddRoleAssignment(assignment);
    }
}
