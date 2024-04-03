namespace PassIn.Application.DTOs.Responses;

public class ResponseAttendeeRegisteredOnEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid EventId { get; set; }
}
