using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AccountEndpointAlbumTests
{
    [TestClass]
    public class ImgurClientWithOAuth2Tests : TestBase
    {
        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task DeleteAlbumAsync_ThrowsImgurException()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var deleted = await endpoint.DeleteAlbumAsync("lzpoZ7a5IPrxvVe");

            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public async Task GetAlbumAsync_WithValidReponse_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var album = await endpoint.GetAlbumAsync("cuta6");

            Assert.IsNotNull(album);
        }

        [TestMethod]
        public async Task GetAlbumCountAsync_WithValidReponse_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var albums = await endpoint.GetAlbumCountAsync();

            Assert.IsTrue(albums >= 1);
        }

        [TestMethod]
        public async Task GetAlbumIdsAsync_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var albums = await endpoint.GetAlbumIdsAsync();

            Assert.IsTrue(albums.Any());
        }

        [TestMethod]
        public async Task GetAlbumsAsync_IsTrue()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var albums = await endpoint.GetAlbumsAsync();

            Assert.IsTrue(albums.Any());
        }
    }
}