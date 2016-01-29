using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilderTests
{
    [TestClass]
    public class ConversationRequestBuilderTests
    {
        [TestMethod]
        public async Task CreateMessageRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ConversationRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}conversations/Bob";

            var request = requestBuilder.CreateMessageRequest(mockUrl, "Hello World!");

            Assert.IsNotNull(request);
            var expected = "body=Hello+World%21";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.AreEqual("https://api.imgur.com/3/conversations/Bob", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateMessageRequest_WithBodyNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ConversationRequestBuilder();
            requestBuilder.CreateMessageRequest("url", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateMessageRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ConversationRequestBuilder();
            requestBuilder.CreateMessageRequest(null, "Test");
        }
    }
}