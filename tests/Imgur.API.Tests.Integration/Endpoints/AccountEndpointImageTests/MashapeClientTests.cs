using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AccountEndpointImageTests
{
    [TestClass]
    public class MashapeClientTests : TestBase
    {
        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task DeleteImageAsync_ThrowsImgurException()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AccountEndpoint(client);

            var deleted = await endpoint.DeleteImageAsync("ra06GZN", "sarah");

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public async Task GetImageAsync_IsNotNull()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AccountEndpoint(client);

            var image = await endpoint.GetImageAsync("ra06GZN", "sarah");

            Assert.IsNotNull(image);
        }
    }
}