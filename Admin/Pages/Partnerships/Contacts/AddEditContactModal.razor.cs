using Clients.Infrastructure.Managers.Partnerships.Contact;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Partners;
using System.Threading.Tasks;

namespace Admin.Pages.Partnerships.Contacts
{
    public partial class AddEditContactModal
    {
        [Inject] private IContactManager ContactManager { get; set; }
        [Parameter] public ContactRequest ContactRequest { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private async Task SaveAsync()
        {
            var response = await ContactManager.Save(ContactRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
