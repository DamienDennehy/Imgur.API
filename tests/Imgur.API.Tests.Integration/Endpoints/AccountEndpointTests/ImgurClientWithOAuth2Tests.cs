using System;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Integration.Endpoints.AccountEndpointTests
{
    [TestClass]
    public class ImgurClientWithOAuth2Tests : TestBase
    {
        [TestMethod]
        public async Task GetAccountAsync_WithDefaultUsername_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var account = await endpoint.GetAccountAsync();

            Assert.IsTrue(account.Url.Equals("ImgurAPIDotNet", StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        public async Task GetAccountAsync_WithUsername_AreEqual()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var account = await endpoint.GetAccountAsync("sarah");

            Assert.IsTrue("sarah".Equals(account.Url, StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        public async Task GetAccountSettingsAsync_IsTrue()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var settings = await endpoint.GetAccountSettingsAsync();

            Assert.IsFalse(settings.PublicImages);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task SendVerificationEmailAsync_IsTrue()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            await endpoint.SendVerificationEmailAsync();
        }

        [TestMethod]
        public async Task UpdateAccountSettingsAsync_IsTrue()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var updated =
                await
                    endpoint.UpdateAccountSettingsAsync("ImgurClient_" + DateTimeOffset.UtcNow, false,
                        albumPrivacy: AlbumPrivacy.Hidden);

            Assert.IsTrue(updated);
        }

        [TestMethod]
        public async Task VerifyEmailAsync_IsTrue()
        {
            var client = new ImgurClient(ClientId, ClientSecret, OAuth2Token);
            var endpoint = new AccountEndpoint(client);

            var verified = await endpoint.VerifyEmailAsync();

            Assert.IsTrue(verified);
        }
    }
}