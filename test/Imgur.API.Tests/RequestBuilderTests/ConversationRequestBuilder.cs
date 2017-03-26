using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class ConversationRequestBuilderTests
    {
        [Fact]
        public async Task CreateMessageRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new ConversationRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}conversations/Bob";

            var request = ConversationRequestBuilder.CreateMessageRequest(mockUrl, "Hello World!");

            Assert.NotNull(request);
            var expected = "body=Hello+World%21";

            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/conversations/Bob", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void CreateMessageRequest_WithBodyNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ConversationRequestBuilder();

            var exception = Record.Exception(() => ConversationRequestBuilder.CreateMessageRequest("url", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "body");
        }

        [Fact]
        public void CreateMessageRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new ConversationRequestBuilder();

            var exception = Record.Exception(() => ConversationRequestBuilder.CreateMessageRequest(null, "Test"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }
    }
}