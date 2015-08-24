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
        /// <param name="oAuth2Authentication">OAuth2 credentials.</param>
        protected ApiAuthenticationBase(string clientId, string clientSecret, IOAuth2Authentication oAuth2Authentication)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret));

            if (oAuth2Authentication == null)
                throw new ArgumentNullException(nameof(oAuth2Authentication));

            ClientId = clientId;
            ClientSecret = clientSecret;
            OAuth2Authentication = oAuth2Authentication;
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
        ///     OAuth2 credentials.
        /// </summary>
        public virtual IOAuth2Authentication OAuth2Authentication { get; private set; }

        /// <summary>
        ///     Sets the OAuth2Authentication and token to be used.
        /// </summary>
        /// <param name="oAuth2Authentication">See <see cref="IOAuth2Authentication" />.</param>
        public virtual void SetOAuth2Authentication(IOAuth2Authentication oAuth2Authentication)
        {
            OAuth2Authentication = oAuth2Authentication;
        }
    }
}