using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Imgur.API.Authentication;

namespace Imgur.API.Endpoints
{
    /// <summary>
    /// Endpoint base implementation.
    /// </summary>
    public abstract class EndpointBase : IEndpoint
    {
        internal const string OAuth2RequiredExceptionMessage = "OAuth authentication required.";

        public IApiClient ApiClient { get; }

        public HttpClient HttpClient { get; }

        protected EndpointBase(IApiClient apiClient, HttpClient httpClient)
        {
            ApiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

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
    }
}
