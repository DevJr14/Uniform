﻿using Blazored.LocalStorage;
using Clients.Infrastructure.Authentication;
using Clients.Infrastructure.Managers;
using Clients.Infrastructure.Managers.Catalogs.Brands;
using Clients.Infrastructure.Managers.Catalogs.Categories;
using Clients.Infrastructure.Managers.Catalogs.Products;
using Clients.Infrastructure.Managers.Catalogs.Tags;
using Clients.Infrastructure.Managers.Partnerships.Address;
using Clients.Infrastructure.Managers.Partnerships.BankAccount;
using Clients.Infrastructure.Managers.Partnerships.Contact;
using Clients.Infrastructure.Managers.Partnerships.Partner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using SharedR.Constants.Permission;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace Admin.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        private const string ClientName = "Api";

        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");

            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        { 
            builder.Services
                .AddAuthorizationCore(options =>
                {
                    RegisterPermissionClaims(options);
                })
                .AddBlazoredLocalStorage()
                .AddMudServices(configuration =>
                {
                    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                    configuration.SnackbarConfiguration.HideTransitionDuration = 100;
                    configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                    configuration.SnackbarConfiguration.VisibleStateDuration = 6000;
                    configuration.SnackbarConfiguration.ShowCloseIcon = true;
                })
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ApplicationStateProvider>()
                .AddScoped<AuthenticationStateProvider, ApplicationStateProvider>()
                .AddManagers()
                .AddTransient<AuthenticationHeaderHandler>()
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(ClientName).EnableIntercept(sp))
                .AddHttpClient(ClientName, client =>
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
                    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseApiUrl"));
                })
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();
            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddTransient<IPartnerManger, PartnerManager>();
            builder.Services.AddTransient<IAddressManager, AddressManager>();
            builder.Services.AddTransient<IContactManager, ContactManager>();
            builder.Services.AddTransient<IBankAccountManager, BankAccountManager>();
            builder.Services.AddTransient<ITagManager, TagManager>();
            builder.Services.AddTransient<ICategoryManager, CategoryManager>();
            //builder.Services.AddTransient<IBrandManager, BrandManager>();
            builder.Services.AddTransient<IProductManager, ProductManager>();
            return builder;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managers = typeof(IManager);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }

        private static void RegisterPermissionClaims(AuthorizationOptions options)
        {
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()));
                }
            }
        }
    }
}
