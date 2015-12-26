using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class NotificationEndpointTests : TestBase
    {
        [TestMethod]
        public async Task GetNotificationAsync_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/notification/12345";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(NotificationEndpointResponses.GetNotificationResponse)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new NotificationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var notification = await endpoint.GetNotificationAsync("12345");

            Assert.IsNotNull(notification);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetNotificationAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);
            await endpoint.GetNotificationAsync("123");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetNotificationAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new NotificationEndpoint(client);
            await endpoint.GetNotificationAsync(null);
        }

        [TestMethod]
        public async Task GetNotificationsAsync_IsNotNull()
        {
            var fakeUrl = "https://api.imgur.com/3/notification?new=false";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(NotificationEndpointResponses.GetNotificationsResponse)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new NotificationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
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
            var endpoint = new NotificationEndpoint(client);
            await endpoint.GetNotificationsAsync();
        }

        [TestMethod]
        public async Task MarkNotificationsViewedAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/notification/12345";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(NotificationEndpointResponses.MarkNotificationViewed)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new NotificationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var marked = await endpoint.MarkNotificationViewedAsync("12345");

            Assert.IsTrue(marked);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task MarkNotificationsViewedAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new NotificationEndpoint(client);
            await endpoint.MarkNotificationsViewedAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task MarkNotificationsViewedAsync_WithOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);
            await endpoint.MarkNotificationsViewedAsync(new List<string> {"456"});
        }

        [TestMethod]
        public async Task MarkNotificationViewedAsync_IsTrue()
        {
            var fakeUrl = "https://api.imgur.com/3/notification/12345";
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(NotificationEndpointResponses.MarkNotificationViewed)
            };

            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new NotificationEndpoint(client,
                new HttpClient(new FakeHttpMessageHandler(fakeUrl, fakeResponse)));
            var marked = await endpoint.MarkNotificationsViewedAsync(new List<string> {"12345", "4445"});

            Assert.IsTrue(marked);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task MarkNotificationViewedAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", FakeOAuth2Token);
            var endpoint = new NotificationEndpoint(client);
            await endpoint.MarkNotificationViewedAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task MarkNotificationViewedAsync_WithOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);
            await endpoint.MarkNotificationViewedAsync("123");
        }
    }
}