using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AccountEndpointImageTests
{
    [TestClass]
    public class ImgurClientTests : TestBase
    {
        [TestMethod]
        public async Task GetImageAsync_IsNotNull()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var image = await endpoint.GetImageAsync("ra06GZN", "sarah");

            Assert.IsNotNull(image);
        }
    }
}