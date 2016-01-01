using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class MemeGenEndpointTests : TestBase
    {
        [TestMethod]
        public async Task GetDefaultMemesAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/memegen/defaults";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MemeGenEndpointResponses.GetDefaultMemesAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new MemeGenEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var memes = await endpoint.GetDefaultMemesAsync().ConfigureAwait(false);

            Assert.IsTrue(memes.Any());
        }
    }
}