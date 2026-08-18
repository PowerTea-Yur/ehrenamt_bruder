namespace Domain.Registrations;

public class Registration
{
  public Guid Id { get; }
  public Guid VolunteerId { get; }
  public Guid EventId { get; }
  public Guid ShiftId { get; }
  public Guid ShiftRoleAssignmentId { get; }
  public RegistrationStatus Status { get; private set; }
  public DateTime JoinedAtUtc { get; }

  private Registration(
      Guid id,
      Guid volunteerId,
      Guid eventId,
      Guid shiftId,
      Guid shiftRoleAssignmentId,
      RegistrationStatus status,
      DateTime joinedAtUtc
  )
  {
    Id = id;
    VolunteerId = volunteerId;
    EventId = eventId;
    ShiftId = shiftId;
    ShiftRoleAssignmentId = shiftRoleAssignmentId;
    Status = status;
    JoinedAtUtc = joinedAtUtc;
  }

  private Registration() { }

  public static Registration CreateConfirmed(
      Guid volunteerId,
      Guid eventId,
      Guid shiftId,
      Guid shiftRoleAssignmentId
  )
  {
    return new Registration(
        Guid.NewGuid(),
        volunteerId,
        eventId,
        shiftId,
        shiftRoleAssignmentId,
        RegistrationStatus.Confirmed,
        DateTime.UtcNow
    );
  }

  public static Registration CreatePending(
      Guid volunteerId,
      Guid eventId,
      Guid shiftId,
      Guid shiftRoleAssignmentId
  )
  {
    return new Registration(
        Guid.NewGuid(),
        volunteerId,
        eventId,
        shiftId,
        shiftRoleAssignmentId,
        RegistrationStatus.Pending,
        DateTime.UtcNow
    );
  }

  public static Registration CreateWaitlisted(
      Guid volunteerId,
      Guid eventId,
      Guid shiftId,
      Guid shiftRoleAssignmentId
  )
  {
    return new Registration(
        Guid.NewGuid(),
        volunteerId,
        eventId,
        shiftId,
        shiftRoleAssignmentId,
        RegistrationStatus.Waitlisted,
        DateTime.UtcNow
    );
  }
}
