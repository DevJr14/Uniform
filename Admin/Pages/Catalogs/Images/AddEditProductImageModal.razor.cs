using Admin.Pages.Catalogs.Products;
using Clients.Infrastructure.Managers.Catalogs.ProductImages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using SharedR.Enums;
using SharedR.Requests;
using SharedR.Requests.Catalogs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Images
{
    public partial class AddEditProductImageModal
    {
        [Inject] private IProductImageManager ProductImageManager { get; set; }
        [Parameter]
        public ProductImageRequest ProductImageRequest { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        private IBrowserFile _file;

        //protected override async Task OnInitializedAsync()
        //{
        //    if (ProductImageRequest.Id != Guid.Empty)
        //    {
        //        await LoadImage();
        //    }
        //}

        //private async Task LoadImage()
        //{
        //    var response = await ProductImageManager.GetById(ProductImageRequest.Id);
        //    if (response.Succeeded)
        //    {
        //        var imageUrl = response.Data.ImageDataURL;
        //    }
        //}

        private async Task SaveAsync()
        {
            var response = await ProductImageManager.Save(ProductImageRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                MudDialog.Close();
            }
            else
            {
                _snackBar.Add(response.Messages[0], Severity.Error);
            }
        }

        private async Task UploadImage(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                ProductImageRequest.ImageDataURL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                ProductImageRequest.UploadRequest = new UploadRequest { Data = buffer, UploadType = UploadType.Product, Extension = extension };
            }
        }

        private void DeleteAsync()
        {
            ProductImageRequest.ImageDataURL = null;
            ProductImageRequest.UploadRequest = new UploadRequest();
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
