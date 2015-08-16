using System;

namespace Imgur.API.Authentication.Impl
{
    /// <summary>
    ///     Imgur API application credentials.
    ///     Register at https://api.imgur.com/oauth2/addclient
    /// </summary>
    public class ImgurAuthentication : ApiAuthenticationBase
    {
        /// <summary>
        ///     Initializes a new instance of the ImgurAuthentication class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        /// <exception cref="ArgumentNullException" />
        public ImgurAuthentication(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret));

            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        /// <summary>
        ///     Initializes a new instance of the ImgurAuthentication class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        /// <param name="oAuth2Authentication">OAuth2 credentials.</param>
        /// <exception cref="ArgumentNullException" />
        public ImgurAuthentication(string clientId, string clientSecret, IOAuth2Authentication oAuth2Authentication)
            : base(oAuth2Authentication)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException(nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException(nameof(clientSecret));

            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        /// <summary>
        ///     The Imgur app's ClientId.
        /// </summary>
        public virtual string ClientId { get; private set; }

        /// <summary>
        ///     The Imgur app's ClientSecret.
        /// </summary>
        public virtual string ClientSecret { get; private set; }
    }
}