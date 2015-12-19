using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
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
            var endpoint = new AlbumEndpoint(client);

            var url = $"{endpoint.ApiClient.EndpointUrl}album/AbcdeF/add";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = endpoint.RequestBuilder.AddAlbumImagesRequest(url, ids);
            var expected = "ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/album/AbcdeF/add", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Put, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AddAlbumImagesRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            var url = $"{endpoint.ApiClient.EndpointUrl}album/AbcdeF/add";
            endpoint.RequestBuilder.AddAlbumImagesRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AddAlbumImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            endpoint.RequestBuilder.AddAlbumImagesRequest(null, null);
        }

        [TestMethod]
        public async Task CreateAlbumRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var url = $"{endpoint.ApiClient.EndpointUrl}album/AbcdeF";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = endpoint.RequestBuilder.CreateAlbumRequest(
                url, "TheTitle", "TheDescription",
                AlbumPrivacy.Hidden, AlbumLayout.Horizontal,
                "io9XpoO", ids);

            var expected =
                "privacy=hidden&layout=horizontal&cover=io9XpoO&title=TheTitle&description=TheDescription&ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/album/AbcdeF", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void CreateAlbumRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            endpoint.RequestBuilder.CreateAlbumRequest(null);
        }

        [TestMethod]
        public void RemoveAlbumImagesRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var url = $"{endpoint.ApiClient.EndpointUrl}album/AbcdeF/remove_images";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = endpoint.RequestBuilder.RemoveAlbumImagesRequest(url, ids);
            var expected = "ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual("https://api.imgur.com/3/album/AbcdeF/remove_images?" + expected,
                request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Delete, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void RemoveAlbumImagesRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            var url = $"{endpoint.ApiClient.EndpointUrl}album/AbcdeF/remove_images";
            endpoint.RequestBuilder.RemoveAlbumImagesRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void RemoveAlbumImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            endpoint.RequestBuilder.RemoveAlbumImagesRequest(null, null);
        }

        [TestMethod]
        public async Task SetAlbumImagesRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var url = $"{endpoint.ApiClient.EndpointUrl}album/AbcdeF";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = endpoint.RequestBuilder.SetAlbumImagesRequest(url, ids);
            var expected = "ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/album/AbcdeF", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SetAlbumImagesRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            var url = $"{endpoint.ApiClient.EndpointUrl}album/AbcdeF";
            endpoint.RequestBuilder.SetAlbumImagesRequest(url, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SetAlbumImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            endpoint.RequestBuilder.SetAlbumImagesRequest(null, null);
        }

        [TestMethod]
        public async Task UpdateAlbumRequest_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);

            var url = $"{endpoint.ApiClient.EndpointUrl}album/AbcdeF";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = endpoint.RequestBuilder.UpdateAlbumRequest(
                url, "TheTitle", "TheDescription",
                AlbumPrivacy.Hidden, AlbumLayout.Horizontal,
                "io9XpoO", ids);

            var expected =
                "privacy=hidden&layout=horizontal&cover=io9XpoO&title=TheTitle&description=TheDescription&ids=Abc%2CDEF%2CXyZ";

            Assert.IsNotNull(request);
            Assert.AreEqual(expected, await request.Content.ReadAsStringAsync());
            Assert.AreEqual("https://api.imgur.com/3/album/AbcdeF", request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Post, request.Method);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UpdateAlbumRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new AlbumEndpoint(client);
            endpoint.RequestBuilder.UpdateAlbumRequest(null);
        }
    }
}