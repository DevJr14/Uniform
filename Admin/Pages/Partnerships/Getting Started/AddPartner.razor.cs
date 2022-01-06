using Clients.Infrastructure.Managers.Partnerships.Partner;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Pages.Partnerships.Getting_Started
{
    public partial class AddPartner
    {
        [Inject] private IPartnerManager PartnerManger { get; set; }
        public PartnerRequest PartnerRequest { get; set; } = new();
        MudDatePicker _picker;

        private async Task SaveAsync()
        {
            var response = await PartnerManger.Save(PartnerRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                Continue(response.Data);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }
        private void Continue(Guid partnerId)
        {
            _navigationManager.NavigateTo($"/partnership/add-address/{partnerId}");
        }
        private void Cancel()
        {
            _navigationManager.NavigateTo("/getting-started");
        }
    }
}
