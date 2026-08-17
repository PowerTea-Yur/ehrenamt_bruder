namespace Domain.Tests.Events;

public class ShiftRoleTests
{
  [Fact]
  public void Constructor_WithValidArguments_CreatesShiftRole()
  {
    var sut = TestObjects.CreateShiftRole();

    Assert.Equal("Boat Driver", sut.Name);
    Assert.True(sut.RequiresApproval);
  }

  [Fact]
  public void Constructor_WithEmptyName_ThrowsArgumentException()
  {
    Assert.Throws<ArgumentException>(() => TestObjects.CreateShiftRole(name: ""));
  }
}
