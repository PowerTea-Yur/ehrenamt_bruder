using Domain.Events;

namespace Domain.Tests.Events;

internal static class TestObjects
{
  public static Shift CreateShift(
      Guid? id = null,
      DateOnly? shiftDate = null,
      TimeOnly? startTime = null,
      TimeOnly? endTime = null
  )
  {
    return new Shift(
        id ?? Guid.NewGuid(),
        shiftDate ?? DateOnly.FromDateTime(DateTime.Now),
        startTime ?? new TimeOnly(9, 0),
        endTime ?? new TimeOnly(11, 0)
    );
  }

  public static ShiftRole CreateShiftRole(
      Guid? id = null,
      string name = "Boat Driver",
      bool requiresApproval = true
  )
  {
    return new ShiftRole(id ?? Guid.NewGuid(), name, requiresApproval);
  }

  public static Event CreateEvent(
      Guid? id = null,
      Guid? organizationId = null,
      string name = "Beach Cleanup",
      string street = "Main Street 1"
  )
  {
    return new Event(
        id ?? Guid.NewGuid(),
        organizationId ?? Guid.NewGuid(),
        name,
        new Location(street)
    );
  }
}
