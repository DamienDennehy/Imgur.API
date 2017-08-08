using System;

namespace Imgur.API.Models
{
    /// <summary>
    ///     An OAuth2 Token used for actions against a user's account.
    /// </summary>
    public interface IOAuth2Token
    {
        /// <summary>
        ///     The user's access token.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        ///     The user's account id.
        /// </summary>
        string AccountId { get; }

        /// <summary>
        ///     The user's account username.
        /// </summary>
        string AccountUsername { get; }

        /// <summary>
        ///     The DateTimeOffset when the token expires. Usually one month from when the token was created.
        /// </summary>
        DateTimeOffset ExpiresAt { get; }

        /// <summary>
        ///     The time in seconds when the token expires. Usually one month from when the token was created.
        /// </summary>
        int ExpiresIn { get; }

        /// <summary>
        ///     The user's refresh token.
        /// </summary>
        string RefreshToken { get; }

        /// <summary>
        ///     The type of token that was requested, usually "bearer".
        /// </summary>
        string TokenType { get; }
    }
}