using System.Configuration;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

namespace Imgur.API.Tests.Integration
{
    public abstract class TestBase
    {
        private static IOAuth2Token _token;
        public string ClientId => ConfigurationManager.AppSettings["ClientId"];
        public string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];
        public string MashapeKey => ConfigurationManager.AppSettings["MashapeKey"];
        public IOAuth2Token OAuth2Token => GetOAuth2Token();
        public string RefreshToken => ConfigurationManager.AppSettings["RefreshToken"];

        private IOAuth2Token GetOAuth2Token()
        {
            if (_token != null)
                return _token;

            var authentication = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new OAuth2Endpoint(authentication);
            _token = endpoint.GetTokenByRefreshTokenAsync(RefreshToken).Result;
            return _token;
        }
    }
}