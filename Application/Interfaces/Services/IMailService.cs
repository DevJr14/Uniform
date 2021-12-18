using SharedR.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
        Task SendWelcomeEmailAsync(MailRequest request);
    }
}
