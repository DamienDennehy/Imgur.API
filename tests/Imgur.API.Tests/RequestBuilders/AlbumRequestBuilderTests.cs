using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Enums;
using Imgur.API.RequestBuilders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.RequestBuilders
{
    [TestClass]
    public class AlbumRequestBuilderTests
    {
        [TestMethod]
        public async Task AddAlbumImagesRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}album/AbcdeF/add";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = requestBuilder.AddAlbumImagesRequest(fakeUrl, ids);
            var expected = "ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.AreEqual("https://api.imgur.com/3/album/AbcdeF/add", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Put, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AddAlbumImagesRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();
            var fakeUrl = $"{client.EndpointUrl}album/AbcdeF/add";
            requestBuilder.AddAlbumImagesRequest(fakeUrl, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AddAlbumImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();
            requestBuilder.AddAlbumImagesRequest(null, new List<string>());
        }

        [TestMethod]
        public async Task CreateAlbumRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}album/AbcdeF";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = requestBuilder.CreateAlbumRequest(
                fakeUrl, "TheTitle", "TheDescription",
                AlbumPrivacy.Hidden, AlbumLayout.Horizontal,
                "io9XpoO", ids);

            var expected =
                "privacy=hidden&layout=horizontal&cover=io9XpoO&title=TheTitle&description=TheDescription&ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.AreEqual("https://api.imgur.com/3/album/AbcdeF", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateAlbumRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();
            requestBuilder.CreateAlbumRequest(null);
        }

        [TestMethod]
        public void RemoveAlbumImagesRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}album/AbcdeF/remove_images";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = requestBuilder.RemoveAlbumImagesRequest(fakeUrl, ids);
            var expected = "https://api.imgur.com/3/album/AbcdeF/remove_images?ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Delete, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void RemoveAlbumImagesRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();
            var fakeUrl = $"{client.EndpointUrl}album/AbcdeF/remove_images";
            requestBuilder.RemoveAlbumImagesRequest(fakeUrl, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void RemoveAlbumImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();
            requestBuilder.RemoveAlbumImagesRequest(null, new List<string>());
        }

        [TestMethod]
        public async Task SetAlbumImagesRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}album/AbcdeF";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = requestBuilder.SetAlbumImagesRequest(fakeUrl, ids);
            var expected = "ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.AreEqual("https://api.imgur.com/3/album/AbcdeF", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SetAlbumImagesRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();
            var fakeUrl = $"{client.EndpointUrl}album/AbcdeF";
            requestBuilder.SetAlbumImagesRequest(fakeUrl, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SetAlbumImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();
            requestBuilder.SetAlbumImagesRequest(null, new List<string>());
        }

        [TestMethod]
        public async Task UpdateAlbumRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var fakeUrl = $"{client.EndpointUrl}album/AbcdeF";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = requestBuilder.UpdateAlbumRequest(
                fakeUrl, "TheTitle", "TheDescription",
                AlbumPrivacy.Hidden, AlbumLayout.Horizontal,
                "io9XpoO", ids);

            var expected =
                "privacy=hidden&layout=horizontal&cover=io9XpoO&title=TheTitle&description=TheDescription&ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.AreEqual("https://api.imgur.com/3/album/AbcdeF", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UpdateAlbumRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();
            requestBuilder.UpdateAlbumRequest(null);
        }
    }
}