using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Enums;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class RequestBuilderBaseTests
    {
        [Fact]
        public void CreateRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");

            var mockUrl = $"{client.EndpointUrl}account/bob";
            var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, mockUrl);

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/account/bob", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Get, request.Method);
        }

        [Fact]
        public void CreateRequest_WithHttpMethodNull_ThrowsArgumentNullException()
        {
            var exception = Record.Exception(() => RequestBuilderBase.CreateRequest(null, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "httpMethod");
        }

        [Fact]
        public void CreateRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var exception = Record.Exception(() => RequestBuilderBase.CreateRequest(HttpMethod.Get, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }
    }
}