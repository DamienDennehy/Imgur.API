using System.Configuration;
using System.Threading.Tasks;
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
        public string RefreshToken => ConfigurationManager.AppSettings["RefreshToken"];

        public async Task<IOAuth2Token> GetOAuth2Token()
        {
            var authentication = new ImgurAuthentication(ClientId, ClientSecret);
            var endpoint = new OAuth2Endpoint(authentication);
            var token = await endpoint.GetTokenByRefreshTokenAsync(RefreshToken);

            return token;
        }
    }
}