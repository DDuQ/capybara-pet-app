using CapybaraPetApp.Domain.Common.JoinTables.Interaction;

namespace CapybaraPetApp.Application.Dtos;

public class InteractionDetailDto
{
    public InteractionType InteractionType { get; private set; }
    public int Quantity { get; private set; }
}