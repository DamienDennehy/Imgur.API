using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AccountEndpointImageTests
{
    [TestClass]
    public class ImgurClientWithOAuth2Tests : TestBase
    {
        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task DeleteImageAsync_ThrowsImgurException()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var deleted = await endpoint.DeleteImageAsync("ra06GZN", "sarah");

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public async Task GetImageAsync_IsNotNull()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var image = await endpoint.GetImageAsync("BJRWQw5");

            Assert.IsNotNull(image);
        }

        [TestMethod]
        public async Task GetImageCountAsync_Any()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var imageCount = await endpoint.GetImageCountAsync();

            Assert.IsTrue(imageCount > 1);
        }

        [TestMethod]
        public async Task GetImageIdsAsync_Any()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var images = await endpoint.GetImageIdsAsync();

            Assert.IsTrue(images.Count() > 1);
        }

        [TestMethod]
        public async Task GetImagesAsync_Any()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var images = await endpoint.GetImagesAsync();

            Assert.IsTrue(images.Any());
        }
    }
}