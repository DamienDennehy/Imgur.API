using System;
using Imgur.API.Models;

namespace Imgur.API.Authentication.Impl
{
    /// <summary>
    ///     OAuth2 credentials.
    ///     Register at https://api.imgur.com/oauth2/addclient
    /// </summary>
    public class OAuth2Authentication : IOAuth2Authentication
    {
        /// <summary>
        ///     Initializes a new instance of the OAuth2Authentication class.
        /// </summary>
        /// <param name="oAuth2ResponseType">Determines if Imgur returns a Code, a PIN code, or an opaque Token.</param>
        public OAuth2Authentication(OAuth2ResponseType oAuth2ResponseType)
        {
            OAuth2ResponseType = oAuth2ResponseType;
        }

        /// <summary>
        ///     Determines if Imgur returns a Code, a PIN code, or an opaque Token.
        /// </summary>
        public virtual OAuth2ResponseType OAuth2ResponseType { get; }

        /// <summary>
        ///     The OAuth2 response from the endpoint. Null if a request hasn't been made yet.
        /// </summary>
        public virtual IOAuth2Token OAuth2Token { get; private set; }

        /// <summary>
        ///     Sets <see cref="OAuth2Token" />.
        /// </summary>
        /// <param name="token">The token.</param>
        public void SetOAuth2Token(IOAuth2Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (token.AccessToken == null)
                throw new ArgumentNullException(nameof(token.AccessToken));

            if (token.AccountId == null)
                throw new ArgumentNullException(nameof(token.AccountId));

            if (token.RefreshToken == null)
                throw new ArgumentNullException(nameof(token.RefreshToken));

            if (token.TokenType == null)
                throw new ArgumentNullException(nameof(token.TokenType));

            OAuth2Token = token;
        }
    }
}