using System.Text.Json.Serialization;

namespace Imgur.API.Models
{
    /// <summary>
    /// The response from an OAuth2 Token request.
    /// </summary>
    public class OAuth2Token : IOAuth2Token
    {
        /// <summary>
        /// The secret key used to access the user's data. 
        /// It can be thought of the user's password and username combined into one, and is used to access the user's account. It expires after 1 month.
        /// </summary>
        [JsonPropertyName("access_token")]
        public virtual string AccessToken { get; set; }

        /// <summary>
        /// The lifetime of the token in seconds.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public virtual int? ExpiresIn { get; set; }

        /// <summary>
        /// The kind of token that is being returned.
        /// </summary>
        [JsonPropertyName("token_type")]
        public virtual string TokenType { get; set; }

        /// <summary>
        /// Used to generate an Access Token.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public virtual string RefreshToken { get; set; }

        /// <summary>
        /// The account's id.
        /// </summary>
        [JsonPropertyName("account_id")]
        public virtual string AccountId { get; set; }

        /// <summary>
        /// The account's username.
        /// </summary>
        [JsonPropertyName("account_username")]
        public virtual string AccountUsername { get; set; }
    }
}
