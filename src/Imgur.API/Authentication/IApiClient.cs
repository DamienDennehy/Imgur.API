using Imgur.API.Models;

namespace Imgur.API.Authentication
{
    /// <summary>
    /// The type of client that will be used for authentication.
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// The client id obtained during application registration.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// The client secret obtained during application registration.
        /// </summary>
        string ClientSecret { get; }

        /// <summary>
        /// An OAuth2 Token used for actions against a user's account.
        /// </summary>
        IOAuth2Token OAuth2Token { get; }

        /// <summary>
        /// The Base Api Address. Typically https://api.imgur.com/3/
        /// </summary>
        string BaseAddress { get; }
    }
}
