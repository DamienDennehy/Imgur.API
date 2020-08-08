using Imgur.API.Models;
using Imgur.API.ResponseConverters;
using Xunit;

namespace Imgur.API.Tests.ResponseTests
{
    public class OAuth2ResponseTests
    {
        [Fact]
        public void ConvertResponse_WithGetTokenResponse_ReturnsOAuth2Token()
        {
            var responseConverter = new ResponseConverter();
            var response = responseConverter.ConvertOAuth2TokenResponse(Mocks.MockOAuth2Responses.GetTokenResponse);
            Assert.NotNull(response);
            Assert.IsType<OAuth2Token>(response);
            Assert.Equal("6e079993b20f45fab3c22ed489a6f454", response.AccessToken);
            Assert.Equal(135798223, response.AccountId);
            Assert.Equal("A8XTgSW8pWrNCFwR", response.AccountUsername);
            Assert.Equal(315360000, response.ExpiresIn);
            Assert.Equal("e325da142cd545298ef68868824d3b8a", response.RefreshToken);
            Assert.Equal("bearer", response.TokenType);
        }

        [Fact]
        public void ConvertResponse_WithGetTokenResponseError_ReturnsOAuth2Token()
        {
            var responseConverter = new ResponseConverter();

            var exception = Record.Exception(() =>
            {
                return responseConverter.ConvertOAuth2TokenResponse(Mocks.MockOAuth2Responses.GetTokenResponseError);
            });
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
            Assert.Contains("Refresh token", exception.Message);
        }
    }
}
