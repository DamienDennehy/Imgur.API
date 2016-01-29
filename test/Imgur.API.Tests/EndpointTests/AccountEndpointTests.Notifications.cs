using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented

namespace Imgur.API.Tests.EndpointTests
{
    public partial class AccountEndpointTests
    {
        [TestMethod]
        public async Task GetNotificationsAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/account/me/notifications?new=false";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAccountEndpointResponses.GetNotifications)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new AccountEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var notifications = await endpoint.GetNotificationsAsync(false).ConfigureAwait(false);

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
            await endpoint.GetNotificationsAsync().ConfigureAwait(false);
        }
    }
}