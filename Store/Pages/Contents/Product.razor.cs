using Microsoft.AspNetCore.Components;
using SharedR.Responses.Catalogs;
using System;

namespace Store.Pages.Contents
{
    public partial class Product
    {
        [Parameter] public ProductResponse ProductResponse { get; set; } = new();

        private void LogA()
        {
            Console.WriteLine("Add to Wish ....");
        }
        private void ProductDetails()
        {
            Console.WriteLine("Go to details....");
            _navigationManager.NavigateTo($"/product-details/{ProductResponse.Id}");
        }
    }
}
