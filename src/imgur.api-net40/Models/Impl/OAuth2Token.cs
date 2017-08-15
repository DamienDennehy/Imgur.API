using System;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     The response from an OAuth2 Token request.
    /// </summary>
    public class OAuth2Token : IOAuth2Token
    {
        /// <summary>
        ///     Initializes a new instance of the OAuth2Token class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="tokenType">The type of token, typically "Bearer".</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="accountUsername">The account username.</param>
        /// <param name="expiresIn">The time in seconds when the token expires. Usually one month from the request.</param>
        public OAuth2Token(string accessToken, string refreshToken, string tokenType, string accountId,
            string accountUsername, int expiresIn)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokenType = tokenType;
            AccountId = accountId;
            AccountUsername = accountUsername;
            ExpiresIn = expiresIn;
        }

        /// <summary>
        ///     The user's access token.
        /// </summary>
        [JsonProperty("access_token")]
        public virtual string AccessToken { get; }

        /// <summary>
        ///     The user's account id.
        /// </summary>
        [JsonProperty("account_id")]
        public virtual string AccountId { get; }

        /// <summary>
        ///     The user's account username.
        /// </summary>
        [JsonProperty("account_username")]
        public virtual string AccountUsername { get; }

        /// <summary>
        ///     The DateTimeOffset when the token expires. Usually one month from the request.
        /// </summary>
        public virtual DateTimeOffset ExpiresAt => DateTimeOffset.UtcNow.AddSeconds(ExpiresIn);

        /// <summary>
        ///     The time in seconds when the token expires. Usually one month from the request.
        /// </summary>
        [JsonProperty("expires_in")]
        public virtual int ExpiresIn { get; }

        /// <summary>
        ///     The user's refresh token.
        /// </summary>
        [JsonProperty("refresh_token")]
        public virtual string RefreshToken { get; }

        /// <summary>
        ///     The type of token that was requested, usually "bearer".
        /// </summary>
        [JsonProperty("token_type")]
        public virtual string TokenType { get; }
    }
}