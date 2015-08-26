using System;
using Imgur.API.Models;

namespace Imgur.API.Authentication.Impl
{
    /// <summary>
    ///     Imgur API application credentials.
    ///     Register at https://api.imgur.com/oauth2/addclient
    /// </summary>
    public class MashapeAuthentication : ApiAuthenticationBase, IMashapeAuthentication
    {
        /// <summary>
        ///     Initializes a new instance of the MashapeAuthentication class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        /// <param name="mashapeKey">The Mashape Key. </param>
        public MashapeAuthentication(string clientId, string clientSecret, string mashapeKey)
            : base(clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(mashapeKey))
                throw new ArgumentNullException(nameof(mashapeKey));

            MashapeKey = mashapeKey;
        }

        /// <summary>
        ///     Initializes a new instance of the MashapeAuthentication class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        /// <param name="mashapeKey">The Mashape Key. </param>
        /// <param name="oAuth2Token">An OAuth2 Token used for actions against a user's account.</param>
        public MashapeAuthentication(string clientId, string clientSecret, string mashapeKey,
            IOAuth2Token oAuth2Token)
            : base(clientId, clientSecret, oAuth2Token)
        {
            if (string.IsNullOrWhiteSpace(mashapeKey))
                throw new ArgumentNullException(nameof(mashapeKey));

            MashapeKey = mashapeKey;
        }

        /// <summary>
        ///     The Mashape Key.
        /// </summary>
        public virtual string MashapeKey { get; }
    }
}