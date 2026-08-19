using Application.Events;
using Domain.Events;

namespace Infrastructure.Persistence.Repositories;

public class EventRepository(AppDbContext context) : IEventRepository
{
  private readonly AppDbContext _context = context;

  public async Task AddAsync(Event @event)
  {
    _context.Events.Add(@event);
    await _context.SaveChangesAsync();
  }
}
