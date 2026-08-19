namespace Api.Events;

public record CreateEventRequest(Guid OrganizationId, string Name, string Street);
