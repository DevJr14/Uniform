using Clients.Infrastructure.Managers.Catalogs.ProductImages;
using Microsoft.AspNetCore.Components;
using SharedR.Requests.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Images
{
    public partial class AddEditProductImage
    {
        [Inject] private IProductImageManager ProductImageManager { get; set; }
        [Parameter]
        public ProductImageRequest ProductImageRequest { get; set; }
    }
}
