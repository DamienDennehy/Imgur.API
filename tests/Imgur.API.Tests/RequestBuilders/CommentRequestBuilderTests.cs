using System;
using System.Net.Http;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class CommentRequestBuilderTests
    {
        [TestMethod]
        public void DeleteCommentRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/bob/comment/xyz";
            var request = endpoint.CommentRequestBuilder.DeleteCommentRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob/comment/xyz", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Delete, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void DeleteCommentRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.CommentRequestBuilder.DeleteCommentRequest(null);
        }

        [TestMethod]
        public void GetCommentCountRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/bob/comments/count";
            var request = endpoint.CommentRequestBuilder.GetCommentCountRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob/comments/count", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetCommentCountRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.CommentRequestBuilder.GetCommentCountRequest(null);
        }

        [TestMethod]
        public void GetCommentIdsRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/bob/comments/ids/newest/2";
            var request = endpoint.CommentRequestBuilder.GetCommentIdsRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob/comments/ids/newest/2", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetCommentIdsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.CommentRequestBuilder.GetCommentIdsRequest(null);
        }

        [TestMethod]
        public void GetCommentRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/bob/comment/xyz";
            var request = endpoint.CommentRequestBuilder.GetCommentRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob/comment/xyz", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetCommentRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.CommentRequestBuilder.GetCommentRequest(null);
        }

        [TestMethod]
        public void GetCommentsRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/bob/comments/newest/2";
            var request = endpoint.CommentRequestBuilder.GetCommentsRequest(url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob/comments/newest/2", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetCommentsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.CommentRequestBuilder.GetCommentsRequest(null);
        }
    }
}