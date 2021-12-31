using System;

namespace SharedR.Requests.Catalogs
{
    public class ProductImageRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string ImageDataURL { get; set; }
        public string AltText { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }
}
