namespace CapybaraPetApp.Application.Abstractions.Dtos;

public class CapybaraDto
{
    private readonly List<InteractionDto> _interactions = new();
    public CapybaraStatsDto _stats;
    public string Name { get; set; }
    public Guid? OwnerId { get; set; }
    public IReadOnlyCollection<InteractionDto> Interactions => _interactions;
    public CapybaraStatsDto Stats => _stats;
}