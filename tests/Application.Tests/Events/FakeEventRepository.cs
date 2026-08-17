using Application.Events;
using Domain.Events;

namespace Application.Tests.Events;

internal class FakeEventRepository : IEventRepository
{
  public List<Event> AddedEvents { get; } = [];

  public Task AddAsync(Event @event)
  {
    AddedEvents.Add(@event);
    return Task.CompletedTask;
  }
}
