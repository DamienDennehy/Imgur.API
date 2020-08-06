using System;
using Imgur.API.Models;

namespace Imgur.API.Authentication
{
    public class ApiClient : IApiClient
    {
        public virtual string ClientId { get; private set; }

        public virtual string ClientSecret { get; private set; }

        public virtual IOAuth2Token OAuth2Token { get; private set; }

        public virtual string BaseAddress { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ApiClient.
        /// Used for public requests (image info, user comments).
        /// </summary>
        /// <param name="clientId"></param>
        public ApiClient(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ArgumentNullException(nameof(clientId));
            }

            ClientId = clientId;
            BaseAddress = "https://api.imgur.com/3/";
        }

        /// <summary>
        /// Initializes a new instance of the ApiClient.
        /// Used for registering an OAuth Token.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public ApiClient(string clientId, string clientSecret) : this(clientId)
        {
            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new ArgumentNullException(nameof(clientSecret));
            }

            ClientSecret = clientSecret;
        }

        public virtual void SetOAuth2Token(IOAuth2Token token)
        {
            if (token == null)
            {
                OAuth2Token = null;
                return;
            }

            if (token.AccessToken == null)
            {
                throw new ArgumentException("token.AccessToken property not set.");
            }

            if (token.RefreshToken == null)
            {
                throw new ArgumentException("token.RefreshToken property not set.");
            }

            if (token.TokenType == null)
            {
                throw new ArgumentException("token.TokenType property not set.");
            }

            if (token.AccountId == 0)
            {
                throw new ArgumentException("token.AccountId property not set.");
            }

            if (token.AccountUsername == null)
            {
                throw new ArgumentException("token.AccountUsername property not set.");
            }

            if (token.ExpiresIn == 0)
            {
                throw new ArgumentException("token.ExpiresIn property not set.");
            }

            OAuth2Token = token;
        }
    }
}
