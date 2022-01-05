using Admin.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Shared
{
    public partial class PersonCard
    {
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        private string FirstName { get; set; }
        private string SecondName { get; set; }
        private string Email { get; set; }
        private char FirstLetterOfName { get; set; }

        [Parameter]
        public string ImageDataUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            var user = state.User;

            Email = user.GetEmail();
            FirstName = user.GetFirstName();
            SecondName = user.GetLastName();
            if (FirstName.Length > 0)
            {
                FirstLetterOfName = FirstName[0];
            }
            var UserId = user.GetUserId();
            var imageResponse = await _userManager.GetProfilePictureAsync(UserId);
            if (imageResponse.Succeeded)
            {
                ImageDataUrl = imageResponse.Data;
            }
        }
    }
}
