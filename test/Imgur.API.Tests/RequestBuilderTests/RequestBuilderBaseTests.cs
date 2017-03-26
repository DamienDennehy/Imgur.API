using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
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
            var requestBuilder = new AccountRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}account/bob";
            var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, mockUrl);

            Assert.NotNull(request);
            Assert.Equal("https://api.imgur.com/3/account/bob", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Get, request.Method);
        }

        [Fact]
        public void CreateRequest_WithHttpMethodNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AccountRequestBuilder();

            var exception = Record.Exception(() => RequestBuilderBase.CreateRequest(null, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "httpMethod");
        }

        [Fact]
        public void CreateRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AccountRequestBuilder();

            var exception = Record.Exception(() => RequestBuilderBase.CreateRequest(HttpMethod.Get, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task ReportItemRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}comment/XysioD/report";
            var request = RequestBuilderBase.ReportItemRequest(mockUrl, ReportReason.Abusive);

            Assert.NotNull(request);
            var expected = "reason=3";

            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/comment/XysioD/report", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void ReportItemRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AccountRequestBuilder();

            var exception = Record.Exception(() => RequestBuilderBase.ReportItemRequest(null, ReportReason.Abusive));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }
    }
}