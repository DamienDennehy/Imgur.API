using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class NotificationsRequestBuilderTests
    {
        [Fact]
        public async Task MarkNotificationsViewedRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new NotificationRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}notifications";
            var ids = new List<string> {"12345", "9867", "45678"};

            var request = NotificationRequestBuilder.MarkNotificationsViewedRequest(mockUrl, ids);
            var expected = "ids=12345%2C9867%2C45678";

            Assert.NotNull(request);
            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/notifications", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void MarkNotificationsViewedRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new NotificationRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}notification";


            var exception = Record.Exception(() => NotificationRequestBuilder.MarkNotificationsViewedRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "notificationIds");
        }

        [Fact]
        public void MarkNotificationsViewedRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new NotificationRequestBuilder();


            var exception =
                Record.Exception(() => NotificationRequestBuilder.MarkNotificationsViewedRequest(null, new List<string>()));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }
    }
}