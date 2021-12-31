using Application.Identity.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities.Partners;
using Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Domain.Contracts;
using Domain.Entities.Catalog;
using Domain.Entities.Promotions;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, ApplicationRoleClaim, IdentityUserToken<string>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;
        public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService, IDateTimeService dateTimeService) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        #region Db Models
        public DbSet<Partner> Partners { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        #endregion
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) 
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users", "Identity");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Roles", "Identity");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");
            });

            builder.Entity<ApplicationRoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims", "Identity");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Identity");
            });

            builder.Entity<Brand>(entity =>
            {
                entity.ToTable(name: "Brands", "catalog");
            });

            builder.Entity<Product>(entity =>
            {
                entity.ToTable(name: "Products", "catalog");
            });

            builder.Entity<Category>(entity =>
            {
                entity.ToTable(name: "Categories", "catalog");
            });

            builder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable(name: "ProductCategories", "catalog");
            });

            builder.Entity<ProductPrice>(entity =>
            {
                entity.ToTable(name: "ProductPrices", "catalog");
            });

            builder.Entity<ProductReview>(entity =>
            {
                entity.ToTable(name: "ProductReviews", "catalog");
            });

            builder.Entity<ProductTag>(entity =>
            {
                entity.ToTable(name: "ProductTags", "catalog");
            });

            builder.Entity<Tag>(entity =>
            {
                entity.ToTable(name: "Tags", "catalog");
            });

            builder.Entity<Discount>(entity =>
            {
                entity.ToTable(name: "Discounts", "promotion");
            });

            builder.Entity<Inventory>(entity =>
            {
                entity.ToTable(name: "Inventories", "catalog");
            });

            builder.Entity<ProductImage>(entity =>
            {
                entity.ToTable(name: "ProductImages", "catalog");
            });
        }
    }
}
