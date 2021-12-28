using Clients.Infrastructure.Managers.Catalogs.Categories;
using Clients.Infrastructure.Managers.Catalogs.Tags;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Tags
{
    public partial class AddEditTagModal
    {
        [Inject] private ITagManager TagManager { get; set; }
        [Parameter] public TagRequest TagRequest { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private async Task SaveAsync()
        {
            var response = await TagManager.Save(TagRequest);
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
