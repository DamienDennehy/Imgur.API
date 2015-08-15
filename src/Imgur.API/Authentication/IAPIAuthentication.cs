using Imgur.API.Models;

namespace Imgur.API.Authentication
{
    /// <summary>
    ///     Imgur or Mashape authentication type.
    /// </summary>
    public interface IApiAuthentication
    {
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