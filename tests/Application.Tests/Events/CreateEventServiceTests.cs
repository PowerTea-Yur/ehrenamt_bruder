using Application.Events;

namespace Application.Tests.Events;

public class CreateEventServiceTests
{
    [Fact]
    public async Task CreateEventAsync_WithValidInput_AddsEventToRepository()
    {
        var repository = new FakeEventRepository();
        var sut = new CreateEventService(repository);

        var eventId = await sut.CreateEventAsync(Guid.NewGuid(), "Beach Cleanup", "Main Street 1");

        var added = Assert.Single(repository.AddedEvents);
        Assert.Equal(eventId, added.Id);
        Assert.Equal("Beach Cleanup", added.Name);
    }
}
