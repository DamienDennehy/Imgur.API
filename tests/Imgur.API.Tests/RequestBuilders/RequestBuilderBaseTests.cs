using System;
using System.Net.Http;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class RequestBuilderBaseTests
    {
        [TestMethod]
        public void CreateRequest_AreEqual()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);

            var url = $"{endpoint.GetEndpointBaseUrl()}account/bob";
            var request = endpoint.RequestBuilder.CreateRequest(HttpMethod.Get, url);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/account/bob", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateRequest_WithHttpMethodNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.RequestBuilder.CreateRequest(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var imgurClient = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurClient);
            endpoint.RequestBuilder.CreateRequest(HttpMethod.Get, null);
        }
    }
}