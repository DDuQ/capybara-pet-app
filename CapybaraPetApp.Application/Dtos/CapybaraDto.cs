namespace CapybaraPetApp.Application.Dtos;

public class CapybaraDto
{
    private readonly List<InteractionDto> _interactions = new();
    public CapybaraStatsDto _stats;
    public string Name { get; set; }
    public Guid? UserId { get; set; }
    public IReadOnlyCollection<InteractionDto> Interactions => _interactions;
    public CapybaraStatsDto Stats => _stats;
}
