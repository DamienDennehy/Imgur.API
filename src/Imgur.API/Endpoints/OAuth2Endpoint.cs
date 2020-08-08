using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints
{
    public class OAuth2Endpoint : EndpointBase, IOAuth2Endpoint
    {
#pragma warning disable S1075 // URIs should not be hardcoded
        internal const string AuthorizationEndpointUrl = "https://api.imgur.com/oauth2/authorize";
        internal const string TokenEndpointUrl = "https://api.imgur.com/oauth2/token";
#pragma warning restore S1075 // URIs should not be hardcoded

        public OAuth2Endpoint(IApiClient apiClient, HttpClient httpClient) : base(
            apiClient, httpClient)
        {
        }

        public string GetAuthorizationUrl(string state = null)
        {
            var url = $"{AuthorizationEndpointUrl}?client_id={_apiClient.ClientId}&response_type=token&state={state}";
            return url;
        }

        public Task<IOAuth2Token> GetTokenByRefreshTokenAsync(string refreshToken,
                                                              CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new ArgumentException("refreshToken", nameof(refreshToken));
            }

            return GetTokenByRefreshTokenInternalAsync(refreshToken, cancellationToken);
        }

        public async Task<IOAuth2Token> GetTokenByRefreshTokenInternalAsync(string refreshToken,
                                                                            CancellationToken cancellationToken = default)
        {
            IOAuth2Token token;

            using (var request = OAuth2RequestBuilder.GetTokenByRefreshTokenRequest(
                TokenEndpointUrl,
                refreshToken,
                _apiClient.ClientId,
                _apiClient.ClientSecret))
            {
                token = await SendRequestAsync<OAuth2Token>(request).ConfigureAwait(false);
            }

            return token;
        }
    }
}
