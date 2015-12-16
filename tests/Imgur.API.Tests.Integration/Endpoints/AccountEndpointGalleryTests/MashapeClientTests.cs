using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AccountEndpointGalleryTests
{
    [TestClass]
    public class MashapeClientTests : TestBase
    {
        [TestMethod]
        public async Task GetAccountGalleryFavoritesAsync_Any()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AccountEndpoint(client);

            var favourites = await endpoint.GetAccountGalleryFavoritesAsync("sarah");

            Assert.IsTrue(favourites.Any());
        }

        [TestMethod]
        public async Task GetAccountSubmissionsAsync_Any()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AccountEndpoint(client);

            var submissions = await endpoint.GetAccountSubmissionsAsync("sarah");

            Assert.IsTrue(submissions.Any());
        }

        [TestMethod]
        public async Task GetGalleryProfileAsync_AnyTrophies()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey);
            var endpoint = new AccountEndpoint(client);

            var profile = await endpoint.GetGalleryProfileAsync("sarah");

            Assert.IsTrue(profile.Trophies.Any());
        }
    }
}