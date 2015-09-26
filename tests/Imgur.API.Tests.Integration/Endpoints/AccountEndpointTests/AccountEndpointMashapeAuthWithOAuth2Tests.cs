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
    public class AccountEndpointMashapeAuthWithOAuth2Tests : TestBase
    {
        [TestMethod]
        public async Task GetAccountAsync_WithDefaultUsername_AreEqual()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new AccountEndpoint(client);

            var account = await endpoint.GetAccountAsync();

            Assert.AreEqual("ImgurAPIDotNet", account.Url);
        }

        [TestMethod]
        public async Task GetAccountFavoritesAsync_Any_IsTrue()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new AccountEndpoint(client);

            var favourites = await endpoint.GetAccountFavoritesAsync();

            Assert.IsTrue(favourites.Any());
        }
    }
}