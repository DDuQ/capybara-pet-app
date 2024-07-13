namespace CapybaraPetApp.Application.Dtos;

public class InteractionDto
{
    public Guid Id { get; set; }
    public InteractionDetailDto InteractionDetail { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid CapybaraId { get; set; }
}

