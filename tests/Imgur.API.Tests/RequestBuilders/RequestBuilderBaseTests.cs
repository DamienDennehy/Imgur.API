using System;
using System.Net.Http;
using Imgur.API.Authentication.Impl;
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
    }
}