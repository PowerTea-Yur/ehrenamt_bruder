using Domain.Events;

namespace Domain.Tests.Events;

public class EventTests
{
  [Fact]
  public void Constructor_WithValidNameAndLocation_SetsStatusToDraft()
  {
    var sut = TestObjects.CreateEvent();

    Assert.Equal(EventStatus.Draft, sut.Status);
  }

  [Fact]
  public void Constructor_WithEmptyName_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => TestObjects.CreateEvent(name: ""));
  }

  [Fact]
  public void Constructor_WithEmptyOrganizationId_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => TestObjects.CreateEvent(organizationId: Guid.Empty));
  }

  [Fact]
  public void Publish_OnFreshEvent_SetsStatusToPublished()
  {
    var sut = TestObjects.CreateEvent();

    sut.Publish();

    Assert.Equal(EventStatus.Published, sut.Status);
  }

  [Fact]
  public void Publish_OnPublishedEvent_ThrowsInvalidOperationException()
  {
    var sut = TestObjects.CreateEvent();
    sut.Publish();

    Assert.Throws<InvalidOperationException>(sut.Publish);
  }

  [Fact]
  public void AssignRoleToShift_WithValidIds_AddsAssignmentToShift()
  {
    var sut = TestObjects.CreateEvent();
    var shift = TestObjects.CreateShift();
    var role = new ShiftRole(Guid.NewGuid(), "Boat Driver", requiresApproval: true);
    sut.AddShift(shift);
    sut.AddShiftRole(role);

    sut.AssignRoleToShift(shift.Id, role.Id, maxVolunteers: 1);

    var assignment = Assert.Single(shift.RoleAssignments);
    Assert.Equal(role.Id, assignment.Role.Id);
    Assert.Equal(1, assignment.MaxVolunteers);
  }

  [Fact]
  public void AssignRoleToShift_WithRoleFromDifferentEvent_ThrowsInvalidOperationException()
  {
    var sut = TestObjects.CreateEvent();
    var shift = TestObjects.CreateShift();
    var role = new ShiftRole(Guid.NewGuid(), "Boat Driver", requiresApproval: true);
    sut.AddShift(shift);
    var roleFromElsewhere = Guid.NewGuid(); // never added via AddShiftRole

    Assert.Throws<ArgumentException>(() =>
        sut.AssignRoleToShift(shift.Id, roleFromElsewhere, maxVolunteers: 1)
    );
  }

  [Fact]
  public void AssignRoleToShift_WithShiftFromDifferentEvent_ThrowsInvalidOperationException()
  {
    var sut = TestObjects.CreateEvent();
    var role = TestObjects.CreateShiftRole();
    sut.AddShiftRole(role);
    var shiftFromElsewhere = Guid.NewGuid(); // never added via AddShift

    Assert.Throws<ArgumentException>(() =>
        sut.AssignRoleToShift(shiftFromElsewhere, role.Id, maxVolunteers: 1)
    );
  }
}
