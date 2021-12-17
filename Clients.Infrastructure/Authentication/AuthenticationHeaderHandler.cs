﻿using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shared.Constants.Storage;

namespace Clients.Infrastructure.Authentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AuthenticationHeaderHandler(ILocalStorageService localStorage)
            => _localStorage = localStorage;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization?.Scheme != "Bearer")
            {
                var savedToken = await _localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken);

                if (!string.IsNullOrWhiteSpace(savedToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
