using System;

namespace SharedR.Responses.Catalogs
{
    public class ProductImageResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string ImageDataURL { get; set; }
        public string AltText { get; set; }
    }
}
