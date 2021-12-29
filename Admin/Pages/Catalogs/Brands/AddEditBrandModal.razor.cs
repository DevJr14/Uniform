using Clients.Infrastructure.Managers.Catalogs.Brands;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Catalogs;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Brands
{
    public partial class AddEditBrandModal
    {
        [Inject] private IBrandManager BrandManager { get; set; }
        [Parameter] public BrandRequest BrandRequest { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private async Task SaveAsync()
        {
            var response = await BrandManager.Save(BrandRequest);
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
