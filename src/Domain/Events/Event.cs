namespace Domain.Events;

public class Event
{
  private readonly List<Shift> _shifts = [];
  private readonly List<ShiftRole> _shiftRoles = [];

  public Guid Id { get; }
  public Guid OrganizationId { get; }
  public string Name { get; }
  public Location Location { get; private set; }
  public EventStatus Status { get; private set; } = EventStatus.Draft;
  public IReadOnlyCollection<Shift> Shifts => _shifts.AsReadOnly();
  public IReadOnlyCollection<ShiftRole> ShiftRoles => _shiftRoles.AsReadOnly();

  public Event(Guid id, Guid organizationId, string name, Location location)
  {
    if (organizationId == Guid.Empty)
      throw new ArgumentException("OrganizationId cannot be empty.", nameof(organizationId));

    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Name cannot be empty.", nameof(name));

    Id = id;
    Name = name;
    Location = location;
  }

  private Event()
  {
    Name = null!;
    Location = null!;
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
    var role = _shiftRoles.FirstOrDefault(r => r.Id == roleId) ??
      throw new ArgumentException("This ShiftRole does not belong to this Event.");

    var shift = _shifts.FirstOrDefault(s => s.Id == shiftId) ??
      throw new ArgumentException("This Shift does not belong to this Event");

    var assignment = new ShiftRoleAssignment(shiftId, role, maxVolunteers);
    shift.AddRoleAssignment(assignment);
  }
}
