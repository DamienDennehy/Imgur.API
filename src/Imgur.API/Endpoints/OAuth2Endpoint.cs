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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "<Pending>")]
        internal const string AuthorizationEndpointUrl = "https://api.imgur.com/oauth2/authorize";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "<Pending>")]
        internal const string TokenEndpointUrl = "https://api.imgur.com/oauth2/token";

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
                throw new ArgumentNullException("refreshToken", nameof(refreshToken));
            }

            return GetTokenByRefreshTokenInternalAsync(refreshToken, cancellationToken);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public async Task<IOAuth2Token> GetTokenByRefreshTokenInternalAsync(string refreshToken,
                                                                            CancellationToken cancellationToken = default)
        {
            using (var request = OAuth2RequestBuilder.GetTokenByRefreshTokenRequest(
                TokenEndpointUrl,
                refreshToken,
                _apiClient.ClientId,
                _apiClient.ClientSecret))
            {
                var httpResponse = await _httpClient.SendAsync(request)
                                                    .ConfigureAwait(false);

                httpResponse.EnsureSuccessStatusCode();

                var response = await httpResponse.Content.ReadAsStringAsync()
                                                         .ConfigureAwait(false);

                return _responseConverter.ConvertOAuth2TokenResponse(response);
            }
        }
    }
}
