using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public partial class AccountEndpointTests : TestBase
    {
        [TestMethod]
        public async Task GetAccountAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/account/bob";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAccountAsync)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var account = await endpoint.GetAccountAsync("bob").ConfigureAwait(false);

            Assert.IsNotNull(account);
            Assert.AreEqual(12456, account.Id);
            Assert.AreEqual("Bob", account.Url);
            Assert.AreEqual(null, account.Bio);
            Assert.AreEqual(4343, account.Reputation);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1229591601), account.Created);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAccountAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAccountAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetAccountSettingsAsync_AreEqual()
        {
            var fakeUrl = "https://api.imgur.com/3/account/me/settings";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAccountSettingsAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var accountSettings = await endpoint.GetAccountSettingsAsync().ConfigureAwait(false);

            Assert.AreEqual(true, accountSettings.AcceptedGalleryTerms);
            Assert.AreEqual("ImgurApiTest@noreply.com", accountSettings.ActiveEmails.First());
            Assert.AreEqual(AlbumPrivacy.Secret, accountSettings.AlbumPrivacy);
            Assert.AreEqual(45454554, accountSettings.BlockedUsers.First().BlockedId);
            Assert.AreEqual("Bob", accountSettings.BlockedUsers.First().BlockedUrl);
            Assert.AreEqual("ImgurApiTest@noreply.com", accountSettings.Email);
            Assert.AreEqual(false, accountSettings.HighQuality);
            Assert.AreEqual(true, accountSettings.MessagingEnabled);
            Assert.AreEqual(false, accountSettings.PublicImages);
            Assert.AreEqual(true, accountSettings.ShowMature);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountSettingsAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetAccountSettingsAsync().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task SendVerificationEmail_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/account/me/verifyemail";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.SendVerificationEmailAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var updated = await endpoint.SendVerificationEmailAsync().ConfigureAwait(false);

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task SendVerificationEmail_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.SendVerificationEmailAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task SendVerificationEmailAsync_ThrowsImgurException()
        {
            var fakeUrl = "https://api.imgur.com/3/account/me/verifyemail";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(AccountEndpointResponses.SendVerificationEmailErrorAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var updated = await endpoint.SendVerificationEmailAsync().ConfigureAwait(false);

            Assert.IsTrue(updated);
        }

        [TestMethod]
        public async Task UpdateAccountSettingsAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/account/me/settings";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.UpdateAccountSettingsAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var updated = await endpoint.UpdateAccountSettingsAsync().ConfigureAwait(false);

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UpdateAccountSettingsAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.UpdateAccountSettingsAsync().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task VerifyEmailAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/account/me/verifyemail";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.VerifyEmailAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var updated = await endpoint.VerifyEmailAsync().ConfigureAwait(false);

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VerifyEmailAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.VerifyEmailAsync().ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task VerifyEmailAsync_ThrowsImgurException()
        {
            var fakeUrl = "https://api.imgur.com/3/account/me/verifyemail";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(AccountEndpointResponses.VerifyEmailErrorAsync)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var updated = await endpoint.VerifyEmailAsync().ConfigureAwait(false);

            Assert.IsTrue(updated);
        }
    }
}