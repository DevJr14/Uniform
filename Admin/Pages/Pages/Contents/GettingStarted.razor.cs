using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Pages.Pages.Contents
{
    public partial class GettingStarted
    {
        private void Cancel()
        {
            _navigationManager.NavigateTo("/");
        }

        private void GetStarted()
        {
            _navigationManager.NavigateTo("partnership/add-partner");
        }
    }
}
