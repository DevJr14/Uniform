using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Pages.Identity
{
    public partial class Account
    {
        private void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }
    }
}
