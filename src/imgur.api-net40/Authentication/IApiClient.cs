using Imgur.API.Models;

namespace Imgur.API.Authentication
{
    /// <summary>
    ///     The type of client that will be used for authentication.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        ///     The Imgur app's ClientId.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        ///     The Imgur app's ClientSecret.
        /// </summary>
        string ClientSecret { get; }

        /// <summary>
        ///     The Endpoint Url.
        ///     https://api.imgur.com/3/ or https://imgur-apiv3.p.mashape.com/3/
        /// </summary>
        string EndpointUrl { get; }

        /// <summary>
        ///     An OAuth2 Token used for actions against a user's account.
        /// </summary>
        IOAuth2Token OAuth2Token { get; }

        /// <summary>
        ///     Remaining credits for the application.
        /// </summary>
        IRateLimit RateLimit { get; }

        /// <summary>
        ///     Sets <see cref="OAuth2Token" />.
        /// </summary>
        /// <param name="token">The token.</param>
        void SetOAuth2Token(IOAuth2Token token);
    }
}