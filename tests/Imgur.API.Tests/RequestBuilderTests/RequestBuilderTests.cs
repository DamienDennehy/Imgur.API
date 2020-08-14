using System;
using System.Net.Http;
using Imgur.API.Authentication;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class RequestBuilderTests
    {
        [Fact]
        public void CreateRequest_Equal()
        {
            var apiClient = new ApiClient("123");

            var mockUrl = $"{apiClient.BaseAddress}account/bob";
            var request = RequestBuilder.CreateRequest(HttpMethod.Get, mockUrl);

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/account/bob", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Get, request.Method);
        }

        [Fact]
        public void CreateRequest_WithHttpMethodNull_ThrowsArgumentNullException()
        {
            var exception = Record.Exception(() => RequestBuilder.CreateRequest(null, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal("httpMethod", argNullException.ParamName);
        }

        [Fact]
        public void CreateRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var exception = Record.Exception(() => RequestBuilder.CreateRequest(HttpMethod.Get, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal("url", argNullException.ParamName);
        }
    }
}
