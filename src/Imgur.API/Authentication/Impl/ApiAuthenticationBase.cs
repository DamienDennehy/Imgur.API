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
        ///     Initializes a new instance of the ApiAuthenticationBase class.
        /// </summary>
        /// <param name="oAuth2Authentication">OAuth2 credentials.</param>
        protected ApiAuthenticationBase(IOAuth2Authentication oAuth2Authentication)
        {
            if (oAuth2Authentication == null)
                throw new ArgumentNullException(nameof(oAuth2Authentication));

            OAuth2Authentication = oAuth2Authentication;
        }

        /// <summary>
        ///     Remaining credits for the application and user.
        /// </summary>
        public virtual IRateLimit RateLimit { get; } = new RateLimit();

        /// <summary>
        ///     OAuth2 credentials.
        /// </summary>
        public virtual IOAuth2Authentication OAuth2Authentication { get; }
    }
}