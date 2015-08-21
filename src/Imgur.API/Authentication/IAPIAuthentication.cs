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
        ///     OAuth2 credentials.
        /// </summary>
        IOAuth2Authentication OAuth2Authentication { get; }

        /// <summary>
        ///     Remaining credits for the application and user.
        /// </summary>
        IRateLimit RateLimit { get; }
    }
}