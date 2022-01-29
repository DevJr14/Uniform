using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Shared
{
    public partial class MainLayout : IDisposable
    {
        private MudBlazorAdminDashboard _theme = new();

        public bool _drawerOpen = true;

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected override void OnInitialized()
        {
            _interceptor.RegisterEvent();
            StateHasChanged();
        }

        private List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Personal", href: "#"),
            new BreadcrumbItem("Dashboard", href: "#"),
        };

        private void Logout()
        {
            var parameters = new DialogParameters
            {
                {nameof(Dialogs.Logout.ContentText), "Logout Confirmation" },
                {nameof(Dialogs.Logout.ButtonText), "Logout"},
                {nameof(Dialogs.Logout.Color), Color.Error},
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            _dialogService.Show<Dialogs.Logout>("Logout", parameters, options);
        }

        public void Dispose()
        {
            _interceptor.DisposeEvent();
        }
    }
}
