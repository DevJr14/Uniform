using Clients.Infrastructure.Managers.Partnerships.Partner;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Responses.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Pages.Partnerships
{
    public partial class PartnerDetails
    {
        [Inject] private IPartnerManger PartnerManager { get; set; }
        [Parameter]
        public Guid PartnerId { get; set; } 
        public PartnerResponse PartnerResponse { get; set; } = new();
        MudDatePicker _picker;
        protected override async Task OnInitializedAsync()
        {
            if(PartnerId != Guid.Empty)
            {
                var response = await PartnerManager.GetById(PartnerId);
                if (response.Succeeded)
                {
                    PartnerResponse = response.Data;
                }
            }
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/pages/partnerships/partners");
        }
        
    }
}
