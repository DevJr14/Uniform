using AutoMapper;
using Domain.Entities.Partners;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;

namespace Application.Mappings.Partners
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactRequest, Contact>();
            CreateMap<Contact, ContactResponse>();
        }
    }
}
