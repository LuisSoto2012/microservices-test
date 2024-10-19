using Account.Application.Features.Accounts.Commands.AddAccount;
using Account.Application.Features.Accounts.Commands.UpdateAccount;
using Account.Application.Features.Accounts.Queries.GetAccountsList;
using Account.Application.Features.Transactions.Commands.AddTransaction;
using Account.Application.Features.Transactions.Commands.UpdateTransaction;
using Account.Application.Features.Transactions.Queries.GetTransactionsList;
using AutoMapper;

namespace Account.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Account, AccountsVm>().ReverseMap();
        CreateMap<Domain.Entities.Account, AddAccountCommand>()
            .ReverseMap()
            .ForMember(dest => dest.CurrentBalance, opt => opt.MapFrom(src => src.InitialBalance))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => true));
        CreateMap<Domain.Entities.Account, UpdateAccountCommand>().ReverseMap();
        CreateMap<Domain.Entities.Transaction, TransactionsVm>().ReverseMap();
        CreateMap<Domain.Entities.Transaction, AddTransactionCommand>()
            .ReverseMap()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<Domain.Entities.Transaction, UpdateTransactionCommand>().ReverseMap();
    }
}