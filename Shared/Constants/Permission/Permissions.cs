using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharedR.Constants.Permission
{
    public static class Permissions
    {
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
            public const string Export = "Permissions.Products.Export";
            public const string Search = "Permissions.Products.Search";
        }

        public static class Brands
        {
            public const string View = "Permissions.Brands.View";
            public const string Create = "Permissions.Brands.Create";
            public const string Edit = "Permissions.Brands.Edit";
            public const string Delete = "Permissions.Brands.Delete";
            public const string Export = "Permissions.Brands.Export";
            public const string Search = "Permissions.Brands.Search";
        }

        public static class Partners
        {
            public const string View = "Permissions.Partners.View";
            public const string Create = "Permissions.Partners.Create";
            public const string Edit = "Permissions.Partners.Edit";
            public const string Delete = "Permissions.Partners.Delete";
            public const string Export = "Permissions.Partners.Export";
            public const string Search = "Permissions.Partners.Search";
            public const string Activate = "Permissions.Partners.Activate";
        }
        public static class ProductCategories
        {
            public const string View = "Permissions.ProductCategories.View";
            public const string Create = "Permissions.ProductCategories.Create";
            public const string Edit = "Permissions.ProductCategories.Edit";
            public const string Delete = "Permissions.ProductCategories.Delete";
            public const string Export = "Permissions.ProductCategories.Export";
            public const string Search = "Permissions.ProductCategories.Search";
        }

        public static class ProductPrices
        {
            public const string View = "Permissions.ProductPrices.View";
            public const string Create = "Permissions.ProductPrices.Create";
            public const string Edit = "Permissions.ProductPrices.Edit";
            public const string Delete = "Permissions.ProductPrices.Delete";
            public const string Export = "Permissions.ProductPrices.Export";
            public const string Search = "Permissions.ProductPrices.Search";
        }

        public static class Inventories
        {
            public const string View = "Permissions.Inventories.View";
            public const string Create = "Permissions.Inventories.Create";
            public const string Edit = "Permissions.Inventories.Edit";
            public const string Delete = "Permissions.Inventories.Delete";
            public const string Export = "Permissions.Inventories.Export";
            public const string Search = "Permissions.Inventories.Search";
        }
        public static class ProductImages
        {
            public const string View = "Permissions.ProductImages.View";
            public const string Create = "Permissions.ProductImages.Create";
            public const string Edit = "Permissions.ProductImages.Edit";
            public const string Delete = "Permissions.ProductImages.Delete";
            public const string Export = "Permissions.ProductImages.Export";
            public const string Search = "Permissions.ProductImages.Search";
        }

        public static class Priorities
        {
            public const string View = "Permissions.Priorities.View";
            public const string Create = "Permissions.Priorities.Create";
            public const string Edit = "Permissions.Priorities.Edit";
            public const string Delete = "Permissions.Priorities.Delete";
            public const string Export = "Permissions.Priorities.Export";
            public const string Search = "Permissions.Priorities.Search";
        }

        public static class Statuses
        {
            public const string View = "Permissions.Statuses.View";
            public const string Create = "Permissions.Statuses.Create";
            public const string Edit = "Permissions.Statuses.Edit";
            public const string Delete = "Permissions.Statuses.Delete";
            public const string Export = "Permissions.Statuses.Export";
            public const string Search = "Permissions.Statuses.Search";
        }

        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string Create = "Permissions.Categories.Create";
            public const string Edit = "Permissions.Categories.Edit";
            public const string Delete = "Permissions.Categories.Delete";
            public const string Export = "Permissions.Categories.Export";
            public const string Search = "Permissions.Categories.Search";
        }

        public static class ProductReviews
        {
            public const string View = "Permissions.ProductReviews.View";
            public const string Create = "Permissions.ProductReviews.Create";
            public const string Edit = "Permissions.ProductReviews.Edit";
            public const string Delete = "Permissions.ProductReviews.Delete";
            public const string Export = "Permissions.ProductReviews.Export";
            public const string Search = "Permissions.ProductReviews.Search";
        }

        public static class ProductTags
        {
            public const string View = "Permissions.ProductTags.View";
            public const string Create = "Permissions.ProductTags.Create";
            public const string Edit = "Permissions.ProductTags.Edit";
            public const string Delete = "Permissions.ProductTags.Delete";
            public const string Export = "Permissions.ProductTags.Export";
            public const string Search = "Permissions.ProductTags.Search";
        }

        public static class Tags
        {
            public const string View = "Permissions.Tags.View";
            public const string Create = "Permissions.Tags.Create";
            public const string Edit = "Permissions.Tags.Edit";
            public const string Delete = "Permissions.Tags.Delete";
            public const string Export = "Permissions.Tags.Export";
            public const string Search = "Permissions.Tags.Search";
        }

        public static class Discounts
        {
            public const string View = "Permissions.Discounts.View";
            public const string Create = "Permissions.Discounts.Create";
            public const string Edit = "Permissions.Discounts.Edit";
            public const string Delete = "Permissions.Discounts.Delete";
            public const string Export = "Permissions.Discounts.Export";
            public const string Search = "Permissions.Discounts.Search";
        }

        public static class Discussions
        {
            public const string View = "Permissions.Discussions.View";
            public const string Create = "Permissions.Discussions.Create";
            public const string Edit = "Permissions.Discussions.Edit";
            public const string Delete = "Permissions.Discussions.Delete";
            public const string Export = "Permissions.Discussions.Export";
            public const string Search = "Permissions.Discussions.Search";
        }

        public static class Documents
        {
            public const string View = "Permissions.Team.View";
            public const string Create = "Permissions.Team.Create";
            public const string Edit = "Permissions.Team.Edit";
            public const string Delete = "Permissions.Team.Delete";
            public const string Search = "Permissions.Team.Search";
        }

        public static class DocumentTypes
        {
            public const string View = "Permissions.DocumentTypes.View";
            public const string Create = "Permissions.DocumentTypes.Create";
            public const string Edit = "Permissions.DocumentTypes.Edit";
            public const string Delete = "Permissions.DocumentTypes.Delete";
            public const string Export = "Permissions.DocumentTypes.Export";
            public const string Search = "Permissions.DocumentTypes.Search";
        }

        public static class DocumentExtendedAttributes
        {
            public const string View = "Permissions.DocumentExtendedAttributes.View";
            public const string Create = "Permissions.DocumentExtendedAttributes.Create";
            public const string Edit = "Permissions.DocumentExtendedAttributes.Edit";
            public const string Delete = "Permissions.DocumentExtendedAttributes.Delete";
            public const string Export = "Permissions.DocumentExtendedAttributes.Export";
            public const string Search = "Permissions.DocumentExtendedAttributes.Search";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Export = "Permissions.Users.Export";
            public const string Search = "Permissions.Users.Search";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string Search = "Permissions.Roles.Search";
        }

        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
            public const string Search = "Permissions.RoleClaims.Search";
        }

        public static class Communication
        {
            public const string Chat = "Permissions.Communication.Chat";
        }

        public static class Preferences
        {
            public const string ChangeLanguage = "Permissions.Preferences.ChangeLanguage";

            //TODO - add permissions
        }

        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";
        }

        public static class Hangfire
        {
            public const string View = "Permissions.Hangfire.View";
        }

        public static class AuditTrails
        {
            public const string View = "Permissions.AuditTrails.View";
            public const string Export = "Permissions.AuditTrails.Export";
            public const string Search = "Permissions.AuditTrails.Search";
        }
        /// <summary>
        /// Returns a list of Permissions.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permssions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permssions.Add(propertyValue.ToString());
            }
            return permssions;
        }
    }
}
