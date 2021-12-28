using AutoMapper;
using Domain.Entities.Partners;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;

namespace Application.Mappings.Partners
{
    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            CreateMap<BankAccountRequest, BankAccount>();
            CreateMap<BankAccount, BankAccountResponse>();
        }
    }
}
