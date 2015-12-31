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
    public class RequestBuilderBaseTests
    {
        [TestMethod]
        public void CreateRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AccountRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}account/bob";
            var request = requestBuilder.CreateRequest(HttpMethod.Get, fakeUrl);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateRequest_WithHttpMethodNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AccountRequestBuilder();
            requestBuilder.CreateRequest(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AccountRequestBuilder();
            requestBuilder.CreateRequest(HttpMethod.Get, null);
        }

        [TestMethod]
        public async Task ReportItemRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CommentRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}comment/XysioD/report";
            var request = requestBuilder.ReportItemRequest(fakeUrl, ReportReason.Abusive);

            Assert.IsNotNull(request);
            var expected = "reason=3";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/comment/XysioD/report", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ReportItemRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AccountRequestBuilder();
            requestBuilder.ReportItemRequest(null, ReportReason.Abusive);
        }
    }
}