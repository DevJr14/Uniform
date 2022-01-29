using Clients.Infrastructure.Managers.Identity.Authentication;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace Clients.Infrastructure.Managers.Interceptors
{
    public class HttpInterceptorManager : IHttpInterceptorManager
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackBar;

        public HttpInterceptorManager(ISnackbar snackBar, NavigationManager navigationManager, IAuthenticationManager authenticationManager, HttpClientInterceptor interceptor)
        {
            _snackBar = snackBar;
            _navigationManager = navigationManager;
            _authenticationManager = authenticationManager;
            _interceptor = interceptor;
        }

        public void DisposeEvent()
        {
            throw new NotImplementedException();
        }
        public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            //To Do: User registration need to be bipassed as well. Update register route.
            if (!absPath.Contains("auth") && !absPath.Contains("token"))
            {
                try
                {
                    var token = await _authenticationManager.TryForceRefreshToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        _snackBar.Add("Refreshed Token.", Severity.Success);
                        e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _snackBar.Add("You are Logged Out.", Severity.Error);
                    await _authenticationManager.Logout();
                    _navigationManager.NavigateTo("/");
                }
            }
        }

    }
}
