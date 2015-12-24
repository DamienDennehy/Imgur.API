using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.RequestBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class CustomGalleryRequestBuilderTests
    {
        [TestMethod]
        public async Task AddCustomGalleryTagsRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();

            var url = $"{client.EndpointUrl}g/custom/add_tags";
            var tags = new List<string> {"Cats", "Dogs", "Seals"};

            var request = requestBuilder.AddCustomGalleryTagsRequest(url, tags);
            var expected = "tags=Cats%2CDogs%2CSeals";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/g/custom/add_tags", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Put, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AddCustomGalleryTagsRequest_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();
            var url = $"{client.EndpointUrl}g/custom/add_tags";
            requestBuilder.AddCustomGalleryTagsRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AddCustomGalleryTagsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CustomGalleryRequestBuilder();
            requestBuilder.AddCustomGalleryTagsRequest(null, null);
        }

        [TestMethod]
        public async Task AddFilteredOutGalleryTagRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();

            var url = $"{client.EndpointUrl}g/block_tag";
            var tag = "Cats";

            var request = requestBuilder.AddFilteredOutGalleryTagRequest(url, tag);
            var expected = "tag=Cats";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/g/block_tag", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AddFilteredOutGalleryTagRequest_WithTagNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();
            var url = $"{client.EndpointUrl}g/block_tag";
            requestBuilder.AddFilteredOutGalleryTagRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AddFilteredOutGalleryTagRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CustomGalleryRequestBuilder();
            requestBuilder.AddFilteredOutGalleryTagRequest(null, null);
        }

        [TestMethod]
        public void RemoveCustomGalleryTagsRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();

            var url = $"{client.EndpointUrl}g/custom/remove_tags";
            var tags = new List<string> {"Cats", "Dogs", "Seals"};

            var request = requestBuilder.RemoveCustomGalleryTagsRequest(url, tags);
            var expected = "https://api.imgur.com/3/g/custom/remove_tags?tags=Cats%2CDogs%2CSeals";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Delete, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void RemoveCustomGalleryTagsRequest_WithTagsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();
            var url = $"{client.EndpointUrl}g/custom/remove_tags";
            requestBuilder.RemoveCustomGalleryTagsRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void RemoveCustomGalleryTagsRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CustomGalleryRequestBuilder();
            requestBuilder.RemoveCustomGalleryTagsRequest(null, null);
        }

        [TestMethod]
        public void RemoveFilteredOutGalleryTagRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();

            var url = $"{client.EndpointUrl}g/unblock_tag";
            var tag = "Cats";

            var request = requestBuilder.RemoveFilteredOutGalleryTagRequest(url, tag);
            var expected = "https://api.imgur.com/3/g/unblock_tag?tag=Cats";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Delete, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void RemoveFilteredOutGalleryTagRequest_WithTagNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new CustomGalleryRequestBuilder();
            var url = $"{client.EndpointUrl}g/unblock_tag";
            requestBuilder.RemoveFilteredOutGalleryTagRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void RemoveFilteredOutGalleryTagRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new CustomGalleryRequestBuilder();
            requestBuilder.RemoveFilteredOutGalleryTagRequest(null, null);
        }
    }
}