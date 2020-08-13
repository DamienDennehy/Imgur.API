﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Converters;

namespace Imgur.API.Endpoints
{
    /// <summary>
    /// Endpoint base implementation.
    /// </summary>
    public abstract class EndpointBase : IEndpoint
    {
        internal const string OAuth2RequiredExceptionMessage = "OAuth authentication required and OAuth2Token not set.";
        internal readonly IApiClient _apiClient;
        internal readonly HttpClient _httpClient;
        internal readonly ResponseConverter _responseConverter;

        /// <summary>
        /// Declares a new instance of an endpoint.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="httpClient"></param>
        protected EndpointBase(IApiClient apiClient, HttpClient httpClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _responseConverter = new ResponseConverter();

            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Accept.Clear();

            //Add OAuth Authentication header
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                "Authorization",
                apiClient.OAuth2Token != null
                    ? $"Bearer {apiClient.OAuth2Token.AccessToken}"
                    : $"Client-ID {apiClient.ClientId}");

            httpClient.BaseAddress = new Uri(apiClient.BaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal virtual Task<T> SendRequestAsync<T>(HttpRequestMessage httpRequestMessage,
                                                     CancellationToken cancellationToken = default)
        {
            if (httpRequestMessage == null)
            {
                throw new ArgumentNullException(nameof(httpRequestMessage));
            }

            return SendRequestInternalAsync<T>(httpRequestMessage, cancellationToken);
        }

        internal virtual async Task<T> SendRequestInternalAsync<T>(HttpRequestMessage httpRequestMessage,
                                                                   CancellationToken cancellationToken = default)
        {
            using (var httpResponse = await _httpClient.SendAsync(httpRequestMessage, cancellationToken)
                                                       .ConfigureAwait(false))
            {
                httpResponse.EnsureSuccessStatusCode();

                var response = await httpResponse.Content.ReadAsStringAsync()
                                                         .ConfigureAwait(false);

                return _responseConverter.ConvertResponse<T>(response);
            }
        }
    }
}
