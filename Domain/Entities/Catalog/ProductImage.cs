using Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Catalog
{
    public class ProductImage : AuditableEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        [Column(TypeName = "text")]
        public string ImageDataURL { get; set; }
        public string AltText { get; set; }
        public Product Product { get; set; }
    }
}
