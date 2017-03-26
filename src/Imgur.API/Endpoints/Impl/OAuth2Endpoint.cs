using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Authorizes account access.
    /// </summary>
    public class OAuth2Endpoint : EndpointBase, IOAuth2Endpoint
    {
        /// <summary>
        ///     Initializes a new instance of the OAuth2Endpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public OAuth2Endpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the OAuth2Endpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal OAuth2Endpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal string AuthorizationEndpointUrl { get; } = "https://api.imgur.com/oauth2/authorize";

        internal OAuth2RequestBuilder RequestBuilder { get; } = new OAuth2RequestBuilder();

        internal string TokenEndpointUrl { get; } = "https://api.imgur.com/oauth2/token";

        /// <summary>
        ///     Creates an authorization url that can be used to authorize access to a user's account.
        /// </summary>
        /// <param name="oAuth2ResponseType">Determines if Imgur returns a Code, a PIN code, or an opaque Token.</param>
        /// <param name="state">
        ///     This optional parameter indicates any state which may be useful to your application upon receipt of
        ///     the response.
        /// </param>
        /// <returns></returns>
        public string GetAuthorizationUrl(OAuth2ResponseType oAuth2ResponseType, string state = null)
        {
            var responseType = oAuth2ResponseType.ToString().ToLowerInvariant();
            var url =
                $"{AuthorizationEndpointUrl}?client_id={ApiClient.ClientId}&response_type={responseType}&state={state}";
            return url;
        }

        /// <summary>
        ///     After the user authorizes, the pin is returned as a code to your application
        ///     via the redirect URL you specified during registration, in the form of a regular query string parameter.
        /// </summary>
        /// <param name="code">The code from the query string.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IOAuth2Token> GetTokenByCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            if (string.IsNullOrWhiteSpace(ApiClient.ClientSecret))
                throw new ArgumentNullException(nameof(ApiClient.ClientSecret));

            IOAuth2Token token;

            using (
                var request = OAuth2RequestBuilder.GetTokenByCodeRequest(TokenEndpointUrl, code, ApiClient.ClientId,
                    ApiClient.ClientSecret))
            {
                token = await SendRequestAsync<OAuth2Token>(request).ConfigureAwait(false);
            }

            return token;
        }

        /// <summary>
        ///     After the user authorizes, they will receive a PIN code that they copy into your app.
        /// </summary>
        /// <param name="pin">The PIN that the user is prompted to enter.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        public async Task<IOAuth2Token> GetTokenByPinAsync(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
                throw new ArgumentNullException(nameof(pin));

            if (string.IsNullOrWhiteSpace(ApiClient.ClientSecret))
                throw new ArgumentNullException(nameof(ApiClient.ClientSecret));

            IOAuth2Token token;

            using (
                var request = OAuth2RequestBuilder.GetTokenByPinRequest(TokenEndpointUrl, pin, ApiClient.ClientId,
                    ApiClient.ClientSecret))
            {
                token = await SendRequestAsync<OAuth2Token>(request).ConfigureAwait(false);
            }

            return token;
        }

        /// <summary>
        ///     If a user has authorized their account but you no longer have a valid access_token for them,
        ///     then a new one can be generated by using the refreshToken.
        ///     <para>
        ///         When your application receives a refresh token, it is important to store
        ///         that refresh token for future use.
        ///     </para>
        ///     <para>
        ///         If your application loses the refresh token, you will have to prompt the user
        ///         for their login information again.
        ///     </para>
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IOAuth2Token> GetTokenByRefreshTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentNullException(nameof(refreshToken));

            if (string.IsNullOrWhiteSpace(ApiClient.ClientSecret))
                throw new ArgumentNullException(nameof(ApiClient.ClientSecret));

            IOAuth2Token token;

            using (
                var request = OAuth2RequestBuilder.GetTokenByRefreshTokenRequest(TokenEndpointUrl, refreshToken,
                    ApiClient.ClientId, ApiClient.ClientSecret))
            {
                token = await SendRequestAsync<OAuth2Token>(request).ConfigureAwait(false);
            }

            return token;
        }
    }
}