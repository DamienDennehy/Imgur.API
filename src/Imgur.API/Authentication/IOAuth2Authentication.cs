using Imgur.API.Models;

namespace Imgur.API.Authentication
{
    /// <summary>
    ///     OAuth2 credentials.
    /// </summary>
    public interface IOAuth2Authentication
    {
        /// <summary>
        ///     The response from an OAuth2 request.
        /// </summary>
        IOAuth2Token OAuth2Token { get; }

        /// <summary>
        ///     Determines if Imgur returns a Code, a PIN code, or an opaque Token.
        /// </summary>
        OAuth2ResponseType OAuth2ResponseType { get; }

        /// <summary>
        ///     Sets <see cref="OAuth2Token" />.
        /// </summary>
        /// <param name="token">The token.</param>
        void SetOAuth2Token(IOAuth2Token token);
    }
}