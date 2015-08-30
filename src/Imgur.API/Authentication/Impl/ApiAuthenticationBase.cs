using System;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Authentication.Impl
{
    /// <summary>
    ///     Base Authentication class.
    /// </summary>
    public abstract class ApiAuthenticationBase : IApiAuthentication
    {
        /// <summary>
        ///     Initializes a new instance of the AuthenticationBase class.
        /// </summary>
        protected ApiAuthenticationBase()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the AuthenticationBase class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected ApiAuthenticationBase(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret));

            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        /// <summary>
        ///     Initializes a new instance of the ApiAuthenticationBase class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        /// <param name="oAuth2Token">OAuth2 credentials.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected ApiAuthenticationBase(string clientId, string clientSecret, IOAuth2Token oAuth2Token)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret));

            if (oAuth2Token == null)
                throw new ArgumentNullException(nameof(oAuth2Token));

            if (oAuth2Token.AccessToken == null)
                throw new ArgumentNullException(nameof(oAuth2Token.AccessToken));

            if (oAuth2Token.AccountId == null)
                throw new ArgumentNullException(nameof(oAuth2Token.AccountId));

            if (oAuth2Token.RefreshToken == null)
                throw new ArgumentNullException(nameof(oAuth2Token.RefreshToken));

            if (oAuth2Token.TokenType == null)
                throw new ArgumentNullException(nameof(oAuth2Token.TokenType));

            ClientId = clientId;
            ClientSecret = clientSecret;
            OAuth2Token = oAuth2Token;
        }

        /// <summary>
        ///     The Imgur app's ClientId.
        /// </summary>
        public virtual string ClientId { get; }

        /// <summary>
        ///     The Imgur app's ClientSecret.
        /// </summary>
        public virtual string ClientSecret { get; }

        /// <summary>
        ///     Remaining credits for the application and user.
        /// </summary>
        public virtual IRateLimit RateLimit { get; } = new RateLimit();

        /// <summary>
        ///     An OAuth2 Token used for actions against a user's account.
        /// </summary>
        public virtual IOAuth2Token OAuth2Token { get; private set; }

        /// <summary>
        ///     Sets the oAuth2Token to be used.
        /// </summary>
        /// <param name="oAuth2Token">See <see cref="IOAuth2Token" />.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void SetOAuth2Token(IOAuth2Token oAuth2Token)
        {
            if (oAuth2Token == null)
            {
                OAuth2Token = null;
                return;
            }

            if (oAuth2Token.AccessToken == null)
                throw new ArgumentNullException(nameof(oAuth2Token.AccessToken));

            if (oAuth2Token.AccountId == null)
                throw new ArgumentNullException(nameof(oAuth2Token.AccountId));

            if (oAuth2Token.RefreshToken == null)
                throw new ArgumentNullException(nameof(oAuth2Token.RefreshToken));

            if (oAuth2Token.TokenType == null)
                throw new ArgumentNullException(nameof(oAuth2Token.TokenType));

            OAuth2Token = oAuth2Token;
        }
    }
}