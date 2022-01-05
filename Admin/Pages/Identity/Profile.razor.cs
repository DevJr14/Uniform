using Admin.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using SharedR.Constants.Storage;
using SharedR.Enums;
using SharedR.Requests.Identity;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Admin.Pages.Identity
{
    public partial class Profile
    {
        [Parameter] public string ImageDataUrl { get; set; }
        private readonly UpdateProfileRequest UpdateProfileRequest = new();
        public string UserId { get; set; }
        private char _firstLetterOfName;
        private IBrowserFile File;

        private async Task UpdateProfileAsync()
        {
            var response = await _userManager.UpdateProfileAsync(UpdateProfileRequest);
            if (response.Succeeded)
            {
                await _authenticationManager.Logout();
                _snackBar.Add("Your Profile has been updated. Please Login to Continue.", Severity.Success);
                _navigationManager.NavigateTo("/");
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;
            UpdateProfileRequest.Email = user.GetEmail();
            UpdateProfileRequest.FirstName = user.GetFirstName();
            UpdateProfileRequest.LastName = user.GetLastName();
            UpdateProfileRequest.PhoneNumber = user.GetPhoneNumber();
            UserId = user.GetUserId();
            var response = await _userManager.GetProfilePictureAsync(UserId);
            if (response.Succeeded)
            {
                ImageDataUrl = response.Data;
            }
            if (UpdateProfileRequest.FirstName.Length > 0)
            {
                _firstLetterOfName = UpdateProfileRequest.FirstName[0];
            }
        }

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            File = e.File;
            if (File != null)
            {
                var extension = Path.GetExtension(File.Name);
                var fileName = $"{UserId}-{Guid.NewGuid()}{extension}";
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                var request = new UpdateProfilePictureRequest { Data = buffer, FileName = fileName, Extension = extension, UploadType = UploadType.ProfilePicture };
                var response = await _userManager.UpdateProfilePictureAsync(request, UserId);
                if (response.Succeeded)
                {
                    await _localStorage.SetItemAsync(StorageConstants.Local.UserImageURL, response.Data);
                    _snackBar.Add("Profile picture added.", Severity.Success);
                    _navigationManager.NavigateTo("/account", true);
                }
                else
                {
                    foreach (var error in response.Messages)
                    {
                        _snackBar.Add(error, Severity.Error);
                    }
                }
            }
        }

        private async Task DeleteAsync()
        {
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), $"{string.Format("Do you want to delete the profile picture of {0}", UpdateProfileRequest.Email)}?"}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var request = new UpdateProfilePictureRequest { Data = null, FileName = string.Empty, UploadType = UploadType.ProfilePicture };
                var data = await _userManager.UpdateProfilePictureAsync(request, UserId);
                if (data.Succeeded)
                {
                    await _localStorage.RemoveItemAsync(StorageConstants.Local.UserImageURL);
                    ImageDataUrl = string.Empty;
                    _snackBar.Add("Profile picture deleted.", Severity.Success);
                    _navigationManager.NavigateTo("/account", true);
                }
                else
                {
                    foreach (var error in data.Messages)
                    {
                        _snackBar.Add(error, Severity.Error);
                    }
                }
            }
        }
    }
}
