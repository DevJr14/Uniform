using Clients.Infrastructure.Managers.Catalogs.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Catalogs;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Categories
{
    public partial class AddEditCategoryModal
    {
        [Inject] private ICategoryManager CategoryManager { get; set; }
        [Parameter] public CategoryRequest CategoryRequest { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private async Task SaveAsync()
        {
            var response = await CategoryManager.Save(CategoryRequest);
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
