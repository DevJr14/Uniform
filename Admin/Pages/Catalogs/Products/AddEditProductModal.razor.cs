using Clients.Infrastructure.Managers.Catalogs.Brands;
using Clients.Infrastructure.Managers.Catalogs.Categories;
using Clients.Infrastructure.Managers.Catalogs.ProductCategories;
using Clients.Infrastructure.Managers.Catalogs.Products;
using Clients.Infrastructure.Managers.Catalogs.ProductTags;
using Clients.Infrastructure.Managers.Catalogs.Tags;
using Clients.Infrastructure.Managers.Partnerships.Partner;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedR.Requests.Catalogs;
using SharedR.Responses.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Pages.Catalogs.Products
{
    public partial class AddEditProductModal
    {
        [Inject] private IProductManager ProductManager { get; set; }
        [Inject] private ICategoryManager CategoryManager { get; set; }
        [Inject] private ITagManager TagManager { get; set; }
        [Inject] private IBrandManager BrandManager { get; set; }
        [Inject] private IPartnerManger PartnerManager { get; set; }
        [Inject] private IProductCategoriesManager ProductCategoriesManager { get; set; }
        [Inject] private IProductTagsManager ProductTagsManager { get; set; }

        [Parameter] public ProductRequest ProductRequest { get; set; } = new();
        public ProductTagsRequest ProductTagsRequest { get; set; } = new();
        public ProductCategoriesRequest ProductCategoriesRequest { get; set; } = new();

        public List<TagResponse> Tags { get; set; } = new();
        public List<CategoryResponse> Categories { get; set; } = new();
        public List<BrandResponse> Brands { get; set; } = new();

        public HashSet<string> SeletedCategoriesIds { get; set; } = new();
        public HashSet<string> SeletedTagsIds { get; set; } = new();

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        MudDatePicker _picker;

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            await LoadBrands();
            await LoadCategories();
            await LoadTags();
        }

        private async Task LoadBrands()
        {
            var response = await BrandManager.GetForPartner();
            if (response.Succeeded)
            {
                Brands = response.Data;
            }
        }

        private async Task LoadTags()
        {
            var response = await TagManager.GetForPartner();
            if (response.Succeeded)
            {
                Tags = response.Data;
            }
        }

        private async Task LoadCategories()
        {
            var response = await CategoryManager.GetForPartner();
            if (response.Succeeded)
            {
                Categories = response.Data;
            }
        }
        private async Task SaveAsync()
        {
            var response = await ProductManager.Save(ProductRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await SaveProductTagsAsync(response.Data);
                await SaveProductCategoriesAsync(response.Data);
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

        private async Task SaveProductTagsAsync(Guid productId)
        {
            List<Guid> tagsIds = new();
            foreach (var name in SeletedTagsIds)
            {
                tagsIds.Add(Tags.FirstOrDefault(t => t.Name == name).Id);

            }
            ProductTagsRequest = new()
            {
                ProductId = productId,
                TagIds = tagsIds
            };
            var response = await ProductTagsManager.Save(ProductTagsRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
            }
            else
            {
                _snackBar.Add(response.Messages[0], Severity.Error);
            }
        }

        private async Task SaveProductCategoriesAsync(Guid productId)
        {
            List<Guid> catIds = new();
            foreach (var name in SeletedCategoriesIds)
            {
                catIds.Add(Categories.FirstOrDefault(c => c.Name == name).Id);
            }
            ProductCategoriesRequest = new()
            {
                ProductId = productId,
                CategoryIds = catIds
            };
            var response = await ProductCategoriesManager.Save(ProductCategoriesRequest);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
            }
            else
            {
                _snackBar.Add(response.Messages[0], Severity.Error);
            }
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
