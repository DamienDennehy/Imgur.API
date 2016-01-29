using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilderTests
{
    [TestClass]
    public class NotificationsRequestBuilderTests
    {
        [TestMethod]
        public async Task MarkNotificationsViewedRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new NotificationRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}notifications";
            var ids = new List<string> {"12345", "9867", "45678"};

            var request = requestBuilder.MarkNotificationsViewedRequest(mockUrl, ids);
            var expected = "ids=12345%2C9867%2C45678";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.AreEqual("https://api.imgur.com/3/notifications", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MarkNotificationsViewedRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new NotificationRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}notification";
            requestBuilder.MarkNotificationsViewedRequest(mockUrl, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void MarkNotificationsViewedRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new NotificationRequestBuilder();
            requestBuilder.MarkNotificationsViewedRequest(null, new List<string>());
        }
    }
}