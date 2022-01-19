using Clients.Infrastructure.Managers.Promotion.Discount;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Promotions;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Admin.Pages.Promotions.Discounts
{
    public partial class AddEditDiscountModal
    {
        [Inject] private IDiscountManager DiscountManager { get; set; }
        [Parameter] public DiscountRequest DiscountRequest { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public CultureInfo _en = CultureInfo.GetCultureInfo("en-US");
        MudDatePicker _picker;

        protected override void OnInitialized()
        {
            StartDate = DiscountRequest.StartDate;
            EndDate = DiscountRequest.EndDate;
        }
        private async Task SaveAsync()
        {
            DiscountRequest.StartDate = (DateTime)StartDate;
            DiscountRequest.EndDate = (DateTime)EndDate;

            var response = await DiscountManager.Save(DiscountRequest);
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
