using AutoMapper;
using Client.Application.Features.Clients.Commands.AddClient;
using Client.Application.Features.Clients.Commands.UpdateClient;
using Client.Application.Features.Clients.Queries.GetClientsList;

namespace Client.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Client, ClientsVm>().ReverseMap();
        CreateMap<Domain.Entities.Client, AddClientCommand>()
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));
        CreateMap<Domain.Entities.Client, UpdateClientCommand>().ReverseMap();
    }
}