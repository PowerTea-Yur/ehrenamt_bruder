using Domain.Registrations;

namespace Domain.Tests.Registrations;

public class RegistrationTests
{
    [Fact]
    public void CreateConfirmed_SetsStatusToConfirmed()
    {
        var sut = Registration.CreateConfirmed(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        Assert.Equal(RegistrationStatus.Confirmed, sut.Status);
    }

    [Fact]
    public void CreatePending_SetsStatusToPending()
    {
        var sut = Registration.CreatePending(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        Assert.Equal(RegistrationStatus.Pending, sut.Status);
    }

    [Fact]
    public void CreateWaitlisted_SetsStatusToWaitlisted()
    {
        var sut = Registration.CreateWaitlisted(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        );

        Assert.Equal(RegistrationStatus.Waitlisted, sut.Status);
    }

    [Fact]
    public void CreateConfirmed_SetsIdsFromArguments()
    {
        var volunteerId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var shiftId = Guid.NewGuid();
        var assignmentId = Guid.NewGuid();

        var sut = Registration.CreateConfirmed(volunteerId, eventId, shiftId, assignmentId);

        Assert.Equal(volunteerId, sut.VolunteerId);
        Assert.Equal(eventId, sut.EventId);
        Assert.Equal(shiftId, sut.ShiftId);
        Assert.Equal(assignmentId, sut.ShiftRoleAssignmentId);
    }
}
