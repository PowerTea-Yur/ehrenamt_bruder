using Application.Events;
using Microsoft.AspNetCore.Mvc;

namespace Api.Events;

[ApiController]
[Route("api/controller")]
public class EventsController(CreateEventService createEventService) : ControllerBase
{
  private readonly CreateEventService _createEventService = createEventService;

  [HttpPost]
  public async Task<IActionResult> CreateEvent(CreateEventRequest request)
  {
    var eventId = await _createEventService.CreateEventAsync(
        request.OrganizationId, request.Name, request.Street);

    return CreatedAtAction(nameof(GetEvent), new { id = eventId }, new { id = eventId });
  }

  [HttpGet("{Id}")]
  public IActionResult GetEvent(Guid id)
  {
    //TODO
    return NotFound();
  }

}
