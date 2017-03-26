using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class CommentRequestBuilderTests
    {
        [Fact]
        public async Task CreateCommentRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}comment/XysioD";
            var request = CommentRequestBuilder.CreateCommentRequest(mockUrl, "Hello World!", "xYxAbcD", "ABCdef");

            Assert.NotNull(request);
            var expected = "image_id=xYxAbcD&comment=Hello+World%21&parent_id=ABCdef";

            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/comment/XysioD", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void CreateCommentRequest_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";

            var exception =
                Record.Exception(() => CommentRequestBuilder.CreateCommentRequest(mockUrl, null, "xYxAbcD", "ABCdef"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "comment");
        }

        [Fact]
        public void CreateCommentRequest_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";

            var exception =
                Record.Exception(() => CommentRequestBuilder.CreateCommentRequest(mockUrl, "Hello World", null, "ABCdef"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "galleryItemId");
        }

        [Fact]
        public void CreateCommentRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CommentRequestBuilder();

            var exception =
                Record.Exception(() => CommentRequestBuilder.CreateCommentRequest(null, "Hello World!", "xYxAbcD", "ABCdef"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task CreateGalleryItemCommentRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}gallery/XysioD/comment";
            var request = CommentRequestBuilder.CreateGalleryItemCommentRequest(mockUrl, "Hello World!");

            Assert.NotNull(request);
            var expected = "comment=Hello+World%21";

            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/gallery/XysioD/comment", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void CreateGalleryItemCommentRequest_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";

            var exception = Record.Exception(() => CommentRequestBuilder.CreateGalleryItemCommentRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "comment");
        }

        [Fact]
        public void CreateGalleryItemCommentRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CommentRequestBuilder();

            var exception = Record.Exception(() => CommentRequestBuilder.CreateGalleryItemCommentRequest(null, "Hello World!"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task CreateReplyRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}comment";
            var request = CommentRequestBuilder.CreateReplyRequest(mockUrl, "Hello World!", "xYxAbcD");

            Assert.NotNull(request);
            var expected = "image_id=xYxAbcD&comment=Hello+World%21";

            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/comment", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void CreateReplyRequest_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";

            var exception = Record.Exception(() => CommentRequestBuilder.CreateReplyRequest(mockUrl, null, "xYxAbcD"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "comment");
        }

        [Fact]
        public void CreateReplyRequest_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";

            var exception = Record.Exception(() => CommentRequestBuilder.CreateReplyRequest(mockUrl, "Hello World", null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "galleryItemId");
        }

        [Fact]
        public void CreateReplyRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CommentRequestBuilder();

            var exception = Record.Exception(() => CommentRequestBuilder.CreateReplyRequest(null, "Hello World!", "xYxAbcD"));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public void VoteCommentRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}comment/XysioD/vote/up";
            var request = RequestBuilderBase.CreateRequest(HttpMethod.Post, mockUrl);

            Assert.NotNull(request);

            Assert.Equal("https://api.imgur.com/3/comment/XysioD/vote/up", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }
    }
}