using Domain.Events;

namespace Domain.Registrations;

public static class RegistrationDecisionService
{
  private static readonly RegistrationStatus[] StatusesCountingTowardCapacity = [RegistrationStatus.Confirmed, RegistrationStatus.Pending];

  public static RegistrationStatus Decide(
      ShiftRoleAssignment assignment,
      ShiftRole role,
      IReadOnlyCollection<Registration> existingRegistrationsForAssignment
  )
  {
    var occupiedSlots = existingRegistrationsForAssignment.Count(r =>
        StatusesCountingTowardCapacity.Contains(r.Status)
    );

    if (occupiedSlots >= assignment.MaxVolunteers)
      return RegistrationStatus.Waitlisted;

    return role.RequiresApproval ? RegistrationStatus.Pending : RegistrationStatus.Confirmed;
  }
}
