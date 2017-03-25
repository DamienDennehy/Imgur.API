using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class NotificationEndpointTests : TestBase
    {
        [Fact]
        public async Task GetNotificationAsync_NotNull()
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

            Assert.NotNull(notification);
        }

        [Fact]
        public async Task GetNotificationAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetNotificationAsync("123").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetNotificationAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetNotificationAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task GetNotificationsAsync_NotNull()
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

            Assert.NotNull(notifications);
            Assert.NotNull(notifications.Messages);
            Assert.NotNull(notifications.Replies);
        }

        [Fact]
        public async Task GetNotificationsAsync_OAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.GetNotificationsAsync().ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task MarkNotificationsViewedAsync_True()
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

            Assert.True(marked);
        }

        [Fact]
        public async Task MarkNotificationsViewedAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.MarkNotificationsViewedAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task MarkNotificationsViewedAsync_WithOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () =>
                            await endpoint.MarkNotificationsViewedAsync(new List<string> {"456"}).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task MarkNotificationViewedAsync_True()
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

            Assert.True(marked);
        }

        [Fact]
        public async Task MarkNotificationViewedAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new NotificationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.MarkNotificationViewedAsync(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task MarkNotificationViewedAsync_WithOAuth2Null_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new NotificationEndpoint(client);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.MarkNotificationViewedAsync("123").ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}