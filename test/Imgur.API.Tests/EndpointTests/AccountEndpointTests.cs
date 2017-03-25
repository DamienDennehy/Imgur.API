using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public partial class AccountEndpointTests : TestBase
    {
        [Fact]
        public async Task GetAccountAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/account/bob";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAccount)
            };

            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var account = await endpoint.GetAccountAsync("bob").ConfigureAwait(false);

            Assert.NotNull(account);
            Assert.Equal(12456, account.Id);
            Assert.Equal("Bob", account.Url);
            Assert.Equal(null, account.Bio);
            Assert.Equal(4343, account.Reputation);
            Assert.Equal(NotorietyLevel.Idolized, account.Notoriety);
            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1229591601), account.Created);
        }

        [Fact]
        public async Task GetAccountAsync_WithDefaultUsernameAndOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception = await Record.ExceptionAsync(
                async () => await endpoint.GetAccountAsync().ConfigureAwait(false))
                .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAccountAsync_WithUsernameNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAccountAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetAccountSettingsAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/account/me/settings";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetAccountSettings)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var accountSettings = await endpoint.GetAccountSettingsAsync().ConfigureAwait(false);

            Assert.Equal("Bob", accountSettings.AccountUrl);
            Assert.Equal(true, accountSettings.AcceptedGalleryTerms);
            Assert.Equal("ImgurApiTest@noreply.com", accountSettings.ActiveEmails.First());
            Assert.Equal(AlbumPrivacy.Secret, accountSettings.AlbumPrivacy);
            Assert.Equal(45454554, accountSettings.BlockedUsers.First().BlockedId);
            Assert.Equal("Bob", accountSettings.BlockedUsers.First().BlockedUrl);
            Assert.Equal("ImgurApiTest@noreply.com", accountSettings.Email);
            Assert.Equal(false, accountSettings.HighQuality);
            Assert.Equal(true, accountSettings.MessagingEnabled);
            Assert.Equal(false, accountSettings.PublicImages);
            Assert.Equal(true, accountSettings.ShowMature);
        }

        [Fact]
        public async Task GetAccountSettingsAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetAccountSettingsAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SendVerificationEmail_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception = await Record.ExceptionAsync(
                async () => await endpoint.SendVerificationEmailAsync().ConfigureAwait(false))
                .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SendVerificationEmail_True()
        {
            var mockUrl = "https://api.imgur.com/3/account/me/verifyemail";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.SendVerificationEmail)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var updated = await endpoint.SendVerificationEmailAsync().ConfigureAwait(false);

            Assert.True(updated);
        }

        [Fact]
        public async Task SendVerificationEmailAsync_ThrowsImgurException()
        {
            var mockUrl = "https://api.imgur.com/3/account/me/verifyemail";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(MockAccountEndpointResponses.SendVerificationEmailError)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.SendVerificationEmailAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public async Task UpdateAccountSettingsAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.UpdateAccountSettingsAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task UpdateAccountSettingsAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/account/me/settings";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.UpdateAccountSettings)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var updated = await endpoint.UpdateAccountSettingsAsync().ConfigureAwait(false);

            Assert.True(updated);
        }

        [Fact]
        public async Task VerifyEmailAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.VerifyEmailAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task VerifyEmailAsync_ThrowsImgurException()
        {
            var mockUrl = "https://api.imgur.com/3/account/me/verifyemail";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(MockAccountEndpointResponses.VerifyEmailError)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.VerifyEmailAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public async Task VerifyEmailAsync_True()
        {
            var mockUrl = "https://api.imgur.com/3/account/me/verifyemail";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.VerifyEmail)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var updated = await endpoint.VerifyEmailAsync().ConfigureAwait(false);

            Assert.True(updated);
        }
    }
}