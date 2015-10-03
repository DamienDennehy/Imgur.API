using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AccountEndpointTests
{
    [TestClass]
    public class AccountEndpointImgurAuthTests : TestBase
    {
        [TestMethod]
        public async Task GetAccountAsync_WithUsername_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var account = await endpoint.GetAccountAsync("sarah");

            Assert.AreEqual("sarah", account.Url.ToLower());
        }

        [TestMethod]
        public async Task GetAccountGalleryFavoritesAsync_Any_IsTrue()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var favourites = await endpoint.GetAccountGalleryFavoritesAsync("sarah");

            Assert.IsTrue(favourites.Any());
        }

        [TestMethod]
        public async Task GetAccountSubmissionsAsync_Any_IsTrue()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var submissions = await endpoint.GetAccountSubmissionsAsync("sarah");

            Assert.IsTrue(submissions.Any());
        }

        [TestMethod]
        public async Task GetGalleryProfileAsync_AnyTrophies_IsTrue()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var profile = await endpoint.GetGalleryProfileAsync("sarah");

            Assert.IsTrue(profile.Trophies.Any());
        }

        [TestMethod]
        public async void GetAlbumsAsync_WithValidReponse_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret);
            var endpoint = new AccountEndpoint(client);

            var albums = await endpoint.GetAlbumsAsync("sarah");

            Assert.AreEqual(50, albums.Count());
        }
    }
}