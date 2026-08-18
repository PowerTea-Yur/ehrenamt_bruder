using Domain.Events;
using Domain.Registrations;

namespace Domain.Tests.Registrations;

public class RegistrationDecisionServiceTests
{
  [Fact]
  public void Decide_WithCapacityAvailableAndNoApprovalRequired_ReturnsConfirmed()
  {
    var role = new ShiftRole(Guid.NewGuid(), "Deckhand", requiresApproval: false);
    var assignment = new ShiftRoleAssignment(Guid.NewGuid(), role, maxVolunteers: 2);
    var existingRegistrations = Array.Empty<Registration>();

    var result = RegistrationDecisionService.Decide(assignment, role, existingRegistrations);

    Assert.Equal(RegistrationStatus.Confirmed, result);
  }

  [Fact]
  public void Decide_WithCapacityAvailableAndApprovalRequired_ReturnsPending()
  {
    var role = new ShiftRole(Guid.NewGuid(), "Boat Driver", requiresApproval: true);
    var assignment = new ShiftRoleAssignment(Guid.NewGuid(), role, maxVolunteers: 2);
    var existingRegistrations = Array.Empty<Registration>();

    var result = RegistrationDecisionService.Decide(assignment, role, existingRegistrations);

    Assert.Equal(RegistrationStatus.Pending, result);
  }

  [Fact]
  public void Decide_WithNoCapacityRemaining_ReturnsWaitlisted()
  {
    var role = new ShiftRole(Guid.NewGuid(), "Boat Driver", requiresApproval: false);
    var assignment = new ShiftRoleAssignment(Guid.NewGuid(), role, maxVolunteers: 1);
    var existingRegistrations = new[]
    {
            Registration.CreateConfirmed(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            ),
        };

    var result = RegistrationDecisionService.Decide(assignment, role, existingRegistrations);

    Assert.Equal(RegistrationStatus.Waitlisted, result);
  }

  [Fact]
  public void Decide_WithPendingRegistrationsFillingCapacity_ReturnsWaitlisted()
  {
    var role = new ShiftRole(Guid.NewGuid(), "Boat Driver", requiresApproval: true);
    var assignment = new ShiftRoleAssignment(Guid.NewGuid(), role, maxVolunteers: 1);
    var existingRegistrations = new[]
    {
            Registration.CreatePending(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            ),
        };

    var result = RegistrationDecisionService.Decide(assignment, role, existingRegistrations);

    Assert.Equal(RegistrationStatus.Waitlisted, result);
  }
}
