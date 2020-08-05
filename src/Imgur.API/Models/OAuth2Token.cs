using System.Text.Json.Serialization;

namespace Imgur.API.Models
{
    /// <summary>
    /// An OAuth2 Token used for actions against a user's account.
    /// </summary>
    public class OAuth2Token : IOAuth2Token
    {
        [JsonPropertyName("access_token")]
        public virtual string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public virtual int? ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public virtual string TokenType { get; set; }

        [JsonPropertyName("refresh_token")]
        public virtual string RefreshToken { get; set; }

        [JsonPropertyName("account_id")]
        public virtual string AccountId { get; set; }

        [JsonPropertyName("account_username")]
        public virtual string AccountUsername { get; set; }
    }
}
