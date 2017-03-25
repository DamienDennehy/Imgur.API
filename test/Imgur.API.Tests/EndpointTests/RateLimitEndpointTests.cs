using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class RateLimitEndpointTests : TestBase
    {
        [Fact]
        public async Task GetRateLimitAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/credits";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockRateLimitEndpointResponses.GetRateLimit)
            };

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));

            var client = new ImgurClient("123", "1234");
            var endpoint = new RateLimitEndpoint(client, httpClient);

            var rateLimit = await endpoint.GetRateLimitAsync().ConfigureAwait(false);

            Assert.NotNull(rateLimit);
            Assert.Equal(10500, rateLimit.ClientLimit);
            Assert.Equal(9500, rateLimit.ClientRemaining);
        }
    }
}