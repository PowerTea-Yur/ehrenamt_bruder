using Domain.Events;

namespace Application.Events;

public class CreateEventService
{
    private readonly IEventRepository _eventRepository;

    public CreateEventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Guid> CreateEventAsync(Guid organizationId, string name, string street)
    {
        var location = new Location(street);
        var newEvent = new Event(Guid.NewGuid(), organizationId, name, location);

        await _eventRepository.AddAsync(newEvent);

        return newEvent.Id;
    }
}
