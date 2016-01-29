using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilderTests
{
    [TestClass]
    public class CommentRequestBuilderTests
    {
        [TestMethod]
        public async Task CreateCommentRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}comment/XysioD";
            var request = requestBuilder.CreateCommentRequest(mockUrl, "Hello World!", "xYxAbcD", "ABCdef");

            Assert.IsNotNull(request);
            var expected = "image_id=xYxAbcD&comment=Hello+World%21&parent_id=ABCdef";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.AreEqual("https://api.imgur.com/3/comment/XysioD", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateCommentRequest_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";
            requestBuilder.CreateCommentRequest(mockUrl, null, "xYxAbcD", "ABCdef");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateCommentRequest_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";
            requestBuilder.CreateCommentRequest(mockUrl, "Hello World", null, "ABCdef");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateCommentRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CommentRequestBuilder();
            requestBuilder.CreateCommentRequest(null, "Hello World!", "xYxAbcD", "ABCdef");
        }

        [TestMethod]
        public async Task CreateGalleryItemCommentRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}gallery/XysioD/comment";
            var request = requestBuilder.CreateGalleryItemCommentRequest(mockUrl, "Hello World!");

            Assert.IsNotNull(request);
            var expected = "comment=Hello+World%21";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.AreEqual("https://api.imgur.com/3/gallery/XysioD/comment", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateGalleryItemCommentRequest_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";
            requestBuilder.CreateGalleryItemCommentRequest(mockUrl, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateGalleryItemCommentRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CommentRequestBuilder();
            requestBuilder.CreateGalleryItemCommentRequest(null, "Hello World!");
        }

        [TestMethod]
        public async Task CreateReplyRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}comment";
            var request = requestBuilder.CreateReplyRequest(mockUrl, "Hello World!", "xYxAbcD");

            Assert.IsNotNull(request);
            var expected = "image_id=xYxAbcD&comment=Hello+World%21";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.AreEqual("https://api.imgur.com/3/comment", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateReplyRequest_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";
            requestBuilder.CreateReplyRequest(mockUrl, null, "xYxAbcD");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateReplyRequest_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}comment";
            requestBuilder.CreateReplyRequest(mockUrl, "Hello World", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateReplyRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CommentRequestBuilder();
            requestBuilder.CreateReplyRequest(null, "Hello World!", "xYxAbcD");
        }

        [TestMethod]
        public void VoteCommentRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}comment/XysioD/vote/up";
            var request = requestBuilder.CreateRequest(HttpMethod.Post, mockUrl);

            Assert.IsNotNull(request);

            Assert.AreEqual("https://api.imgur.com/3/comment/XysioD/vote/up", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }
    }
}