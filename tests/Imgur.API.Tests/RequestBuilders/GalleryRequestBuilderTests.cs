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
    public class GalleryRequestBuilderTests : TestBase
    {
        [TestMethod]
        public async Task PublishToGalleryRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new GalleryRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}gallery/XysioD";
            var request = requestBuilder.PublishToGalleryRequest(fakeUrl, "Hello World!", "Funny", true, true);

            Assert.IsNotNull(request);
            var expected = "title=Hello+World%21&topic=Funny&terms=true&mature=true";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/gallery/XysioD", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void PublishToGalleryRequest_WithTitleNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();
            requestBuilder.PublishToGalleryRequest("url", null);
        }

        [TestMethod]
        public async Task PublishToGalleryRequest_WithTitleOnly_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new GalleryRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}gallery/XysioD";
            var request = requestBuilder.PublishToGalleryRequest(fakeUrl, "Hello World!");

            Assert.IsNotNull(request);
            var expected = "title=Hello+World%21";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/gallery/XysioD", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void PublishToGalleryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();
            requestBuilder.PublishToGalleryRequest(null, "test");
        }

        [TestMethod]
        public async Task SearchGalleryAdvancedRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new GalleryRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}gallery/search";
            var request = requestBuilder.SearchGalleryAdvancedRequest(fakeUrl, "star wars", "luke han leia", "Obi-Wan",
                "Kirk", ImageFileType.Anigif, ImageSize.Lrg);

            Assert.IsNotNull(request);
            var expected =
                "q_all=star+wars&q_any=luke+han+leia&q_exactly=Obi-Wan&q_not=Kirk&q_type=anigif&q_size_px=lrg";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/gallery/search", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SearchGalleryAdvancedRequest_WithQueryNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();
            requestBuilder.SearchGalleryAdvancedRequest("Xio");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SearchGalleryAdvancedRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();
            requestBuilder.SearchGalleryAdvancedRequest(null, "dfdfd");
        }

        [TestMethod]
        public async Task SearchGalleryRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new GalleryRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}gallery/search";
            var request = requestBuilder.SearchGalleryRequest(fakeUrl, "star wars");

            Assert.IsNotNull(request);
            var expected = "q=star+wars";

            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/gallery/search", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Get, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SearchGalleryRequest_WithQueryNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();
            requestBuilder.SearchGalleryRequest("Xio", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SearchGalleryRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new GalleryRequestBuilder();
            requestBuilder.SearchGalleryRequest(null, "dfdfd");
        }
    }
}