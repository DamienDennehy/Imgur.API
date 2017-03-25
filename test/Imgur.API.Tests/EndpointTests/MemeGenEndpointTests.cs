using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class MemeGenEndpointTests : TestBase
    {
        [Fact]
        public async Task GetDefaultMemesAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/memegen/defaults";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockMemeGenEndpointResponses.GetDefaultMemes)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new MemeGenEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var memes = await endpoint.GetDefaultMemesAsync().ConfigureAwait(false);

            Assert.True(memes.Any());
        }
    }
}