using Imgur.API.Models;

namespace Imgur.API.Authentication
{
    /// <summary>
    ///     Imgur or Mashape authentication type.
    /// </summary>
    public interface IApiAuthentication
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
        ///     An OAuth2 Token used for actions against a user's account.
        /// </summary>
        IOAuth2Token OAuth2Token { get; }

        /// <summary>
        ///     Remaining credits for the application and user.
        /// </summary>
        IRateLimit RateLimit { get; }

        /// <summary>
        ///     Sets <see cref="OAuth2Token" />.
        /// </summary>
        /// <param name="token">The token.</param>
        void SetOAuth2Token(IOAuth2Token token);
    }
}