using Clients.Infrastructure.Managers.Identity.Identity.Roles;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Identity;
using System.Threading.Tasks;

namespace Admin.Pages.Identity
{
    public partial class AddEditRoleModal
    {
        [Inject] private IRoleManager RoleManager { get; set; }

        [Parameter] public RoleRequest RoleModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        public void Cancel()
        {
            MudDialog.Cancel();
        }
        
        private async Task SaveAsync()
        {
            var response = await RoleManager.SaveAsync(RoleModel);
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
    }
}
