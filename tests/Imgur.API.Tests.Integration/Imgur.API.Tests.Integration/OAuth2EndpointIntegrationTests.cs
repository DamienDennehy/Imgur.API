using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace Imgur.API.Tests.Integration
{
    public class OAuth2EndpointIntegrationTests : IAssemblyFixture<ApiClientAssemblyFixture>
    {
        private readonly ApiClientAssemblyFixture _fixture;

        public OAuth2EndpointIntegrationTests(ApiClientAssemblyFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void OAuth2Endpoint_GetAccessTokenFromRefreshToken_GetsToken()
        {
            var oAuth2Token = _fixture.GetOAuth2Token();

            Assert.NotNull(oAuth2Token);
            Assert.NotNull(oAuth2Token.AccessToken);
        }
    }
}
