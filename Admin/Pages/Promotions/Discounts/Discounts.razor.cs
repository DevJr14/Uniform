using Clients.Infrastructure.Managers.Promotion.Discount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Constants.Permission;
using SharedR.Requests.Promotions;
using SharedR.Responses.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Admin.Pages.Promotions.Discounts
{
    public partial class Discounts
    {
        [Inject] private IDiscountManager DiscountManager { get; set; }
        public List<DiscountResponse> DiscountResponses { get; set; } = new();
        public DiscountResponse Discount { get; set; } = new();
        public DiscountRequest DiscountRequest { get; set; } = new();

        private ClaimsPrincipal _currentUser;
        private bool _canCreateDiscounts;
        private bool _canEditDiscounts;
        private bool _canDeleteDiscounts;
        private bool _canExportDiscounts;
        private bool _canSearchDiscounts;
        private bool _loaded;

        private string _searchString = "";

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateDiscounts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Discounts.Create)).Succeeded;
            _canEditDiscounts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Discounts.Edit)).Succeeded;
            _canDeleteDiscounts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Discounts.Delete)).Succeeded;
            _canExportDiscounts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Discounts.Export)).Succeeded;
            _canSearchDiscounts = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Discounts.Search)).Succeeded;

            await GetDiscounts();
        }

        private async Task GetDiscounts()
        {
            var response = await DiscountManager.GetForPartner();
            _loaded = true;
            if (response.Succeeded)
            {
                DiscountResponses = response.Data;
            }
        }

        private bool Search(DiscountResponse discount)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (discount.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }

        private async Task Delete(Guid id)
        {
            string deleteContent = "Are you sure you want to delete discount?";
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await DiscountManager.Delete(id);
                if (response.Succeeded)
                {
                    _snackBar.Add(response.Messages[0], Severity.Success);
                    await GetDiscounts();
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

        private async Task InvokeModal(Guid id)
        {
            var parameters = new DialogParameters();
            if (id != Guid.Empty)
            {
                var discount = DiscountResponses.FirstOrDefault(c => c.Id == id);
                if (discount != null)
                {
                    parameters.Add(nameof(AddEditDiscountModal.DiscountRequest), new DiscountRequest
                    {
                        Id = discount.Id,
                        PartnerId = discount.PartnerId,
                        Name = discount.Name,
                        Percentage = discount.Percentage,
                        StartDate = discount.StartDate,
                        EndDate = discount.EndDate
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditDiscountModal>(id == Guid.Empty ? "Create" : "Edit", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetDiscounts();
            }
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
