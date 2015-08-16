using System;

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
        /// <param name="mashapeKey">The Mashape Key. </param>
        public MashapeAuthentication(string clientId, string mashapeKey)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));

            if (string.IsNullOrWhiteSpace(mashapeKey))
                throw new ArgumentNullException(nameof(mashapeKey));

            ClientId = clientId;
            MashapeKey = mashapeKey;
        }

        /// <summary>
        ///     Initializes a new instance of the MashapeAuthentication class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="mashapeKey">The Mashape Key. </param>
        /// <param name="oAuth2Authentication">OAuth2 credentials.</param>
        public MashapeAuthentication(string clientId, string mashapeKey, IOAuth2Authentication oAuth2Authentication)
            : base(oAuth2Authentication)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));

            if (string.IsNullOrWhiteSpace(mashapeKey))
                throw new ArgumentNullException(nameof(mashapeKey));

            ClientId = clientId;
            MashapeKey = mashapeKey;
        }

        /// <summary>
        ///     The Imgur app's ClientId.
        /// </summary>
        public virtual string ClientId { get; }

        /// <summary>
        ///     The Mashape Key.
        /// </summary>
        public virtual string MashapeKey { get; }
    }
}