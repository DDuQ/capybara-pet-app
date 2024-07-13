using AutoMapper;
using CapybaraPetApp.Application.Dtos;
using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Capybara, CapybaraDto>()
            .ForMember(dest => dest._stats, opt => opt.MapFrom(src => new CapybaraStatsDto(src.Stats.Happiness, src.Stats.Health, src.Stats.Energy)));
        CreateMap<CapybaraStats, CapybaraStatsDto>();
        CreateMap<InteractionDetail, InteractionDetailDto>();
        CreateMap<Interaction, InteractionDto>();
        CreateMap<ItemDetail, ItemDetailDto>();
        CreateMap<Item, ItemDto>();
        CreateMap<UserAchievement, UserAchievementDto>();
        CreateMap<User,UserDto>();
    }
}
