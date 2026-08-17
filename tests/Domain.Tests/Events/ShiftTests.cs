namespace Domain.Tests.Events;

public class ShiftTests
{
  [Fact]
  public void Constructor_WithValidArguments_CreatesShift()
  {
    var shiftDate = DateOnly.FromDateTime(DateTime.Now);
    var startTime = new TimeOnly(9, 0);
    var endTime = new TimeOnly(11, 0);

    var sut = TestObjects.CreateShift(
        shiftDate: shiftDate,
        startTime: startTime,
        endTime: endTime
    );

    Assert.Equal(shiftDate, sut.ShiftDate);
    Assert.Equal(startTime, sut.StartTime);
    Assert.Equal(endTime, sut.EndTime);
    Assert.False(sut.IsCancelled);
    Assert.Empty(sut.RoleAssignments);
  }

  [Fact]
  public void Constructor_WithGreaterStartTime_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() =>
        TestObjects.CreateShift(startTime: new TimeOnly(13, 0), endTime: new TimeOnly(11, 0))
    );
  }

  [Fact]
  public void Cancel_OnFreshShift_SetsIsCancelledToTrue()
  {
    var sut = TestObjects.CreateShift();

    sut.Cancel();

    Assert.True(sut.IsCancelled);
  }

  [Fact]
  public void Cancel_OnCanceledShift_ThrowsInvalidOperationException()
  {
    var sut = TestObjects.CreateShift();
    sut.Cancel();

    Assert.Throws<InvalidOperationException>(sut.Cancel);
  }
}
