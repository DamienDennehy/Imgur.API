using System.Configuration;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

namespace Imgur.API.Tests.Integration
{
    public abstract class TestBase
    {
        private IOAuth2Token _oAuth2Token;
        public string ClientId => ConfigurationManager.AppSettings["ClientId"];
        public string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];
        public string MashapeKey => ConfigurationManager.AppSettings["MashapeKey"];
        public string RefreshToken => ConfigurationManager.AppSettings["RefreshToken"];

        public async Task<IOAuth2Token> GetOAuth2Token()
        {
            if (_oAuth2Token != null)
                return _oAuth2Token;

            var authentication = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new OAuth2Endpoint(authentication);
            _oAuth2Token = await endpoint.GetTokenByRefreshTokenAsync(RefreshToken);
            return _oAuth2Token;
        }
    }
}