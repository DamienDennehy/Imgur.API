﻿using System;
using Imgur.API.Models;

namespace Imgur.API.Authentication
{
    /// <summary>
    /// The client that will be used for authentication, containing ClientId, ClientSecret, etc.
    /// </summary>
    public class ApiClient : IApiClient
    {
        /// <summary>
        /// The client id obtained during application registration.
        /// </summary>
        public virtual string ClientId { get; private set; }

        /// <summary>
        /// The client secret obtained during application registration.
        /// </summary>
        public virtual string ClientSecret { get; private set; }

        /// <summary>
        /// An OAuth2 Token used for actions against a user's account.
        /// </summary>
        public virtual IOAuth2Token OAuth2Token { get; private set; }

        /// <summary>
        /// The Base Api Address. Typically https://api.imgur.com/3/
        /// </summary>
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

        /// <summary>
        /// Sets the OAuth2Token to be used for authentication.
        /// </summary>
        /// <param name="token"></param>
        public virtual void SetOAuth2Token(IOAuth2Token token)
        {
            if (token == null)
            {
                OAuth2Token = null;
                return;
            }

            if (token.AccessToken == null)
            {
                throw new ArgumentNullException(nameof(token), "token.AccessToken property not set.");
            }

            if (token.RefreshToken == null)
            {
                throw new ArgumentNullException(nameof(token), "token.RefreshToken property not set.");
            }

            if (token.TokenType == null)
            {
                throw new ArgumentNullException(nameof(token), "token.TokenType property not set.");
            }

            if (token.AccountId == 0)
            {
                throw new ArgumentNullException(nameof(token), "token.AccountId property not set.");
            }

            if (token.AccountUsername == null)
            {
                throw new ArgumentNullException(nameof(token), "token.AccountUsername property not set.");
            }

            if (token.ExpiresIn == 0)
            {
                throw new ArgumentNullException(nameof(token), "token.ExpiresIn property not set.");
            }

            OAuth2Token = token;
        }
    }
}
