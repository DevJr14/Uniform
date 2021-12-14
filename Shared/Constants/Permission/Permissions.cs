﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shared.Permission
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

        public static class Companies
        {
            public const string View = "Permissions.Companies.View";
            public const string Create = "Permissions.Companies.Create";
            public const string Edit = "Permissions.Companies.Edit";
            public const string Delete = "Permissions.Companies.Delete";
            public const string Export = "Permissions.Companies.Export";
            public const string Search = "Permissions.Companies.Search";
        }
        public static class Sites
        {
            public const string View = "Permissions.Sites.View";
            public const string Create = "Permissions.Sites.Create";
            public const string Edit = "Permissions.Sites.Edit";
            public const string Delete = "Permissions.Sites.Delete";
            public const string Export = "Permissions.Sites.Export";
            public const string Search = "Permissions.Sites.Search";
        }

        public static class Employees
        {
            public const string View = "Permissions.Employees.View";
            public const string Create = "Permissions.Employees.Create";
            public const string Edit = "Permissions.Employees.Edit";
            public const string Delete = "Permissions.Employees.Delete";
            public const string Export = "Permissions.Employees.Export";
            public const string Search = "Permissions.Employees.Search";
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

        public static class Clients
        {
            public const string View = "Permissions.Clients.View";
            public const string Create = "Permissions.Clients.Create";
            public const string Edit = "Permissions.Clients.Edit";
            public const string Delete = "Permissions.Clients.Delete";
            public const string Export = "Permissions.Clients.Export";
            public const string Search = "Permissions.Clients.Search";
        }

        public static class Projects
        {
            public const string View = "Permissions.Projects.View";
            public const string Create = "Permissions.Projects.Create";
            public const string Edit = "Permissions.Projects.Edit";
            public const string Delete = "Permissions.Projects.Delete";
            public const string Export = "Permissions.Projects.Export";
            public const string Search = "Permissions.Projects.Search";
        }

        public static class Tasks
        {
            public const string View = "Permissions.Tasks.View";
            public const string Create = "Permissions.Tasks.Create";
            public const string Edit = "Permissions.Tasks.Edit";
            public const string Delete = "Permissions.Tasks.Delete";
            public const string Export = "Permissions.Tasks.Export";
            public const string Search = "Permissions.Tasks.Search";
        }

        public static class Teams
        {
            public const string View = "Permissions.Tasks.View";
            public const string Create = "Permissions.Tasks.Create";
            public const string Edit = "Permissions.Tasks.Edit";
            public const string Delete = "Permissions.Tasks.Delete";
            public const string Export = "Permissions.Tasks.Export";
            public const string Search = "Permissions.Tasks.Search";
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
