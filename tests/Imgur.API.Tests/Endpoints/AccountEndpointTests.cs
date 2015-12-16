using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public partial class AccountEndpointTests
    {
        [TestMethod]
        public async Task GetAccountAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAccountResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/bob"), fakeResponse);

            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var account = await endpoint.GetAccountAsync("bob");

            Assert.IsNotNull(account);
            Assert.AreEqual(12456, account.Id);
            Assert.AreEqual("Bob", account.Url);
            Assert.AreEqual(null, account.Bio);
            Assert.AreEqual(4343, account.Reputation);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1229591601), account.Created);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            await endpoint.GetAccountAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            await endpoint.GetAccountAsync(null);
        }

        [TestMethod]
        public async Task GetAccountSettingsAsync_AreEqual()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetAccountSettingsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/me/settings"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var imgurClient = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var accountSettings = await endpoint.GetAccountSettingsAsync();

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
        public async Task GetAccountSettingsAsync_OAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            await endpoint.GetAccountSettingsAsync();
        }

        [TestMethod]
        public async Task SendVerificationEmail_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.SendVerificationEmailResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/me/verifyemail"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var imgurClient = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var updated = await endpoint.SendVerificationEmailAsync();

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task SendVerificationEmail_OAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            await endpoint.SendVerificationEmailAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task SendVerificationEmailAsync_ThrowsImgurException()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(AccountEndpointResponses.SendVerificationEmailErrorResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/me/verifyemail"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var imgurClient = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var updated = await endpoint.SendVerificationEmailAsync();

            Assert.IsTrue(updated);
        }

        [TestMethod]
        public async Task UpdateAccountSettingsAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.UpdateAccountSettingsResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/me/settings"), fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var imgurClient = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var updated = await endpoint.UpdateAccountSettingsAsync();

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UpdateAccountSettingsAsync_OAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            await endpoint.UpdateAccountSettingsAsync();
        }

        [TestMethod]
        public async Task VerifyEmailAsync_IsTrue()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.VerifyEmailResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/me/verifyemail"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var imgurClient = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var updated = await endpoint.VerifyEmailAsync();

            Assert.IsTrue(updated);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VerifyEmailAsync_OAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            await endpoint.VerifyEmailAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task VerifyEmailAsync_ThrowsImgurException()
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(AccountEndpointResponses.VerifyEmailErrorResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("https://api.imgur.com/3/account/me/verifyemail"),
                fakeResponse);

            var fakeOAuth2Handler = new FakeOAuth2TokenHandler();
            var imgurClient = new ImgurClient("123", "1234", fakeOAuth2Handler.GetOAuth2TokenCodeResponse());
            var endpoint = new AccountEndpoint(imgurClient, new HttpClient(fakeHttpMessageHandler));
            var updated = await endpoint.VerifyEmailAsync();

            Assert.IsTrue(updated);
        }
    }
}