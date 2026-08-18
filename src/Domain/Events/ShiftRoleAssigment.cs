namespace Domain.Events;

public class ShiftRoleAssignment
{
  public Guid Id { get; }
  public ShiftRole Role { get; }
  public int MaxVolunteers { get; }

  public ShiftRoleAssignment(Guid id, ShiftRole role, int maxVolunteers)
  {
    if (maxVolunteers <= 0)
      throw new ArgumentException(
          "Number of maximum Volunteers needs to be greater than 0",
          nameof(maxVolunteers)
      );

    Id = id;
    Role = role ?? throw new ArgumentNullException(nameof(role));
    MaxVolunteers = maxVolunteers;
  }

  private ShiftRoleAssignment()
  {
    Role = null!;
  }
}
