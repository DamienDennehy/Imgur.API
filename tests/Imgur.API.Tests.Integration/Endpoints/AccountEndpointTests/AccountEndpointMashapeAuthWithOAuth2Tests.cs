using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
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

            Assert.AreEqual("ImgurAPIDotNet".ToLower(), account.Url.ToLower());
        }

        [TestMethod]
        public async Task GetAccountFavoritesAsync_Any_IsTrue()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new AccountEndpoint(client);

            var favourites = await endpoint.GetAccountFavoritesAsync();

            Assert.IsTrue(favourites.Any());
        }

        [TestMethod]
        public async Task GetAccountSettingsAsync_IsTrue()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new AccountEndpoint(client);

            var settings = await endpoint.GetAccountSettingsAsync();

            Assert.IsFalse(settings.PublicImages);
        }


        [TestMethod]
        public async Task UpdateAccountSettingsAsync_IsTrue()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new AccountEndpoint(client);

            var updated = await endpoint.UpdateAccountSettingsAsync(bio: "ImgurClient_" + DateTimeOffset.UtcNow.ToString(), publicImages: false, albumPrivacy: AlbumPrivacy.Hidden);

            Assert.IsTrue(updated);
        }

        [TestMethod]
        public async Task GetGalleryProfileAsync_WithDefaultUsername_AnyTrophies_IsFalse()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new AccountEndpoint(client);

            var profile = await endpoint.GetGalleryProfileAsync();

            Assert.IsFalse(profile.Trophies.Any());
        }

        [TestMethod]
        public async Task VerifyEmailAsync_IsTrue()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new AccountEndpoint(client);

            var verified = await endpoint.VerifyEmailAsync();

            Assert.IsTrue(verified);
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
        public async Task SendVerificationEmailAsync_IsTrue()
        {
            var client = new MashapeClient(ClientId, ClientSecret, MashapeKey, await GetOAuth2Token());
            var endpoint = new AccountEndpoint(client);

            var sent = await endpoint.SendVerificationEmailAsync();
        }

    }
}