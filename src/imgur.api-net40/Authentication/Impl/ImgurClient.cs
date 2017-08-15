using Imgur.API.Models;

namespace Imgur.API.Authentication.Impl
{
    /// <summary>
    ///     Imgur API application credentials.
    ///     Register at https://api.imgur.com/oauth2/addclient
    /// </summary>
    public class ImgurClient : ApiClientBase, IImgurClient
    {
        /// <summary>
        ///     Initializes a new instance of the ImgurClient class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        public ImgurClient(string clientId) : base(clientId)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ImgurClient class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        public ImgurClient(string clientId, string clientSecret) : base(clientId, clientSecret)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ImgurClient class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="oAuth2Token">An OAuth2 Token used for actions against a user's account.</param>
        public ImgurClient(string clientId, IOAuth2Token oAuth2Token)
            : base(clientId, oAuth2Token)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ImgurClient class.
        /// </summary>
        /// <param name="clientId">The Imgur app's ClientId. </param>
        /// <param name="clientSecret">The Imgur app's ClientSecret.</param>
        /// <param name="oAuth2Token">An OAuth2 Token used for actions against a user's account.</param>
        public ImgurClient(string clientId, string clientSecret, IOAuth2Token oAuth2Token)
            : base(clientId, clientSecret, oAuth2Token)
        {
        }

        /// <summary>
        ///     The Endpoint Url.
        ///     https://api.imgur.com/3/
        /// </summary>
        public override string EndpointUrl { get; } = "https://api.imgur.com/3/";
    }
}