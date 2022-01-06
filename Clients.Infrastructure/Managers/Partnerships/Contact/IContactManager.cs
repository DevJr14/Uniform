using SharedR.Wrapper;
using SharedR.Requests.Partners;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Infrastructure.Managers.Partnerships.Contact
{
    public interface IContactManager : IManager
    {
        Task<IResult<List<ContactResponse>>> GetAll();
        Task<IResult<List<ContactResponse>>> GetForPartner(Guid partnerId);
        Task<IResult<ContactResponse>> GetById(Guid contactId);
        Task<IResult<List<ContactResponse>>> GetForUser(Guid userId);
        Task<IResult<Guid>> Save(ContactRequest contactRequest);
        Task<IResult<Guid>> Delete(Guid contactId);
    }
}
