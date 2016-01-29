using System;
using System.Collections.Generic;
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
    [TestClass]
    public class NotificationEndpointTests : TestBase
    {
        [TestMethod]
        public async Task GetNotificationAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/notification/12345";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockNotificationEndpointResponses.GetNotification)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var notification = await endpoint.GetNotificationAsync("12345").ConfigureAwait(false);

            Assert.IsNotNull(notification);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetNotificationAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);
            await endpoint.GetNotificationAsync("123").ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetNotificationAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client);
            await endpoint.GetNotificationAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetNotificationsAsync_IsNotNull()
        {
            var mockUrl = "https://api.imgur.com/3/notification?new=false";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockNotificationEndpointResponses.GetNotifications)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
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
            var endpoint = new NotificationEndpoint(client);
            await endpoint.GetNotificationsAsync().ConfigureAwait(false);
        }

        [TestMethod]
        public async Task MarkNotificationsViewedAsync_IsTrue()
        {
            var mockUrl = "https://api.imgur.com/3/notification/12345";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockNotificationEndpointResponses.MarkNotificationViewed)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var marked = await endpoint.MarkNotificationViewedAsync("12345").ConfigureAwait(false);

            Assert.IsTrue(marked);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task MarkNotificationsViewedAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client);
            await endpoint.MarkNotificationsViewedAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task MarkNotificationsViewedAsync_WithOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);
            await endpoint.MarkNotificationsViewedAsync(new List<string> {"456"}).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task MarkNotificationViewedAsync_IsTrue()
        {
            var mockUrl = "https://api.imgur.com/3/notification";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockNotificationEndpointResponses.MarkNotificationViewed)
            };

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client,
                new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var marked =
                await endpoint.MarkNotificationsViewedAsync(new List<string> {"12345", "4445"}).ConfigureAwait(false);

            Assert.IsTrue(marked);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task MarkNotificationViewedAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client);
            await endpoint.MarkNotificationViewedAsync(null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task MarkNotificationViewedAsync_WithOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);
            await endpoint.MarkNotificationViewedAsync("123").ConfigureAwait(false);
        }
    }
}