using Domain.Events;

namespace Application.Events;

public interface IEventRepository
{
  Task AddAsync(Event @event);
}
