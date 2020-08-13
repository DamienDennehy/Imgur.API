using Imgur.API.Converters;
using Imgur.API.Models;

namespace Imgur.API.Tests.Mocks
{
    public static class MockOAuth2Token
    {
        public static IOAuth2Token GetOAuth2Token()
        {
            var responseConverter = new ResponseConverter();
            return responseConverter.ConvertOAuth2TokenResponse(MockOAuth2Responses.GetTokenResponse);
        }
    }
}
