using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.FakeResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Endpoints
{
    public partial class AccountEndpointTests : TestBase
    {
        [TestMethod]
        public async Task GetNotificationsAsync_IsNotNull()
        {
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(AccountEndpointResponses.GetNotifications)
            };

            FakeHttpMessageHandler.AddFakeResponse(
                new Uri("https://api.imgur.com/3/account/me/notifications?new=false"), fakeResponse);

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(FakeHttpMessageHandler));
            var notifications = await endpoint.GetNotificationsAsync(false);

            Assert.IsNotNull(notifications);
            Assert.IsNotNull(notifications.Messages);
            Assert.IsNotNull(notifications.Replies);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetNotificationsAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(client);
            await endpoint.GetNotificationsAsync();
        }
    }
}