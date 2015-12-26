using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Enums;
using Imgur.API.RequestBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class CommentRequestBuilderTests
    {
        [TestMethod]
        public async Task CreateCommentRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}comment/XysioD";
            var request = requestBuilder.CreateCommentRequest(fakeUrl, "Hello World!", "xYxAbcD", "ABCdef");

            Assert.IsNotNull(request);
            var expected = "image_id=xYxAbcD&comment=Hello+World%21&parent_id=ABCdef";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/comment/XysioD", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateCommentRequest_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var fakeUrl = $"{client.EndpointUrl}comment";
            requestBuilder.CreateCommentRequest(fakeUrl, null, "xYxAbcD", "ABCdef");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateCommentRequest_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var fakeUrl = $"{client.EndpointUrl}comment";
            requestBuilder.CreateCommentRequest(fakeUrl, "Hello World", null, "ABCdef");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateCommentRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CommentRequestBuilder();
            requestBuilder.CreateCommentRequest(null, "Hello World!", "xYxAbcD", "ABCdef");
        }

        [TestMethod]
        public async Task CreateReplyRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}comment";
            var request = requestBuilder.CreateReplyRequest(fakeUrl, "Hello World!", "xYxAbcD");

            Assert.IsNotNull(request);
            var expected = "image_id=xYxAbcD&comment=Hello+World%21";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/comment", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateReplyRequest_WithCommentNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var fakeUrl = $"{client.EndpointUrl}comment";
            requestBuilder.CreateReplyRequest(fakeUrl, null, "xYxAbcD");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateReplyRequest_WithImageIdNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();
            var fakeUrl = $"{client.EndpointUrl}comment";
            requestBuilder.CreateReplyRequest(fakeUrl, "Hello World", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateReplyRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CommentRequestBuilder();
            requestBuilder.CreateReplyRequest(null, "Hello World!", "xYxAbcD");
        }

        [TestMethod]
        public async Task ReportCommentRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}comment/XysioD/report";
            var request = requestBuilder.ReportCommentRequest(fakeUrl, ReportReason.Abusive);

            Assert.IsNotNull(request);
            var expected = "reason=3";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/comment/XysioD/report", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        public void VoteCommentRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}comment/XysioD/vote/up";
            var request = requestBuilder.CreateRequest(HttpMethod.Post, fakeUrl);

            Assert.IsNotNull(request);

            Assert.AreEqual("https://api.imgur.com/3/comment/XysioD/vote/up", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }
    }
}