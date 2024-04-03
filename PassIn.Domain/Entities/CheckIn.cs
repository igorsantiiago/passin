using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Domain.Entities;

public class CheckIn
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Created_at { get; set; }
    public Guid Attendee_Id { get; set; }

    [ForeignKey("Attendee_Id")]
    public Attendee Attendee { get; set; } = null!;
}
