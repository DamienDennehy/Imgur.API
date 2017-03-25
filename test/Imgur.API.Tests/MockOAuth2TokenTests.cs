using Xunit;

namespace Imgur.API.Tests
{
    public class MockOAuth2TokenHandlerTests : TestBase
    {
        [Fact]
        public void GetOAuth2TokenCodeResponse_Equal()
        {
            var token = MockOAuth2Token;

            Assert.NotNull(token);
            Assert.Equal("CodeResponse", token.AccessToken);
            Assert.Equal(2419200, token.ExpiresIn);
            Assert.Equal("bearer", token.TokenType);
            Assert.Equal("Bob", token.AccountUsername);
            Assert.Equal("45344", token.AccountId);
            Assert.Equal("2132d34234jkljj84ce0c16fjkljfsdfdc70", token.RefreshToken);
        }
    }
}