using System.Configuration;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

namespace Imgur.API.Tests.Integration
{
    public abstract class TestBase
    {
        public string ClientId => ConfigurationManager.AppSettings["ClientId"];
        public string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];
        public string MashapeKey => ConfigurationManager.AppSettings["MashapeKey"];
        public IOAuth2Token OAuth2Token => GetOAuth2Token();
        public string RefreshToken => ConfigurationManager.AppSettings["RefreshToken"];

        private IOAuth2Token GetOAuth2Token()
        {
            var authentication = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new OAuth2Endpoint(authentication);
            return endpoint.GetTokenByRefreshTokenAsync(RefreshToken).Result;
        }
    }
}