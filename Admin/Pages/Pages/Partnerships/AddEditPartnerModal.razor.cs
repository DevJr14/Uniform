using Clients.Infrastructure.Managers.Partnerships.Partner;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Partners;
using System;
using System.Threading.Tasks;

namespace Admin.Pages.Pages.Partnerships
{
    public partial class AddEditPartnerModal
    {
        [Inject] private IPartnerManger PartnerManger { get; set; }
        [Parameter] public PartnerRequest PartnerRequest { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        MudDatePicker _picker;

        private async Task SaveAsync()
        {
            var response = await PartnerManger.Save(PartnerRequest);
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
