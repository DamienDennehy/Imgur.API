using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Enums;
using Imgur.API.RequestBuilders;
using Xunit;

namespace Imgur.API.Tests.RequestBuilderTests
{
    public class AlbumRequestBuilderTests
    {
        [Fact]
        public async Task AddAlbumImagesRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}album/AbcdeF/add";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = AlbumRequestBuilder.AddAlbumImagesRequest(mockUrl, ids);
            var expected = "ids=Abc%2CDEF%2CXyZ";

            Assert.NotNull(request);
            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/album/AbcdeF/add", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Put, request.Method);
        }

        [Fact]
        public void AddAlbumImagesRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}album/AbcdeF/add";

            var exception = Record.Exception(() => AlbumRequestBuilder.AddAlbumImagesRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "imageIds");
        }

        [Fact]
        public void AddAlbumImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();

            var exception = Record.Exception(() => AlbumRequestBuilder.AddAlbumImagesRequest(null, new List<string>()));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task CreateAlbumRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}album/AbcdeF";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = AlbumRequestBuilder.CreateAlbumRequest(
                mockUrl, "TheTitle", "TheDescription",
                AlbumPrivacy.Hidden, AlbumLayout.Horizontal,
                "io9XpoO", ids);

            var expected =
                "privacy=hidden&layout=horizontal&cover=io9XpoO&title=TheTitle&description=TheDescription&ids=Abc%2CDEF%2CXyZ";

            Assert.NotNull(request);
            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/album/AbcdeF", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void CreateAlbumRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();
            var exception = Record.Exception(() => AlbumRequestBuilder.CreateAlbumRequest(null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public void RemoveAlbumImagesRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}album/AbcdeF/remove_images";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = AlbumRequestBuilder.RemoveAlbumImagesRequest(mockUrl, ids);
            var expected = "https://api.imgur.com/3/album/AbcdeF/remove_images?ids=Abc%2CDEF%2CXyZ";

            Assert.NotNull(request);
            Assert.Equal(expected, request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Delete, request.Method);
        }

        [Fact]
        public void RemoveAlbumImagesRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}album/AbcdeF/remove_images";

            var exception = Record.Exception(() => AlbumRequestBuilder.RemoveAlbumImagesRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "imageIds");
        }

        [Fact]
        public void RemoveAlbumImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();

            var exception = Record.Exception(() => AlbumRequestBuilder.RemoveAlbumImagesRequest(null, new List<string>()));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task SetAlbumImagesRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}album/AbcdeF";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = AlbumRequestBuilder.SetAlbumImagesRequest(mockUrl, ids);
            var expected = "ids=Abc%2CDEF%2CXyZ";

            Assert.NotNull(request);
            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/album/AbcdeF", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void SetAlbumImagesRequest_WithIdsNull_ThrowsArgumentNullException()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();
            var mockUrl = $"{client.EndpointUrl}album/AbcdeF";

            var exception = Record.Exception(() => AlbumRequestBuilder.SetAlbumImagesRequest(mockUrl, null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "imageIds");
        }

        [Fact]
        public void SetAlbumImagesRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();

            var exception = Record.Exception(() => AlbumRequestBuilder.SetAlbumImagesRequest(null, new List<string>()));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }

        [Fact]
        public async Task UpdateAlbumRequest_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var requestBuilder = new AlbumRequestBuilder();

            var mockUrl = $"{client.EndpointUrl}album/AbcdeF";
            var ids = new List<string> {"Abc", "DEF", "XyZ"};

            var request = AlbumRequestBuilder.UpdateAlbumRequest(
                mockUrl, "TheTitle", "TheDescription",
                AlbumPrivacy.Hidden, AlbumLayout.Horizontal,
                "io9XpoO", ids);

            var expected =
                "privacy=hidden&layout=horizontal&cover=io9XpoO&title=TheTitle&description=TheDescription&ids=Abc%2CDEF%2CXyZ";

            Assert.NotNull(request);
            Assert.Equal(expected, await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            Assert.Equal("https://api.imgur.com/3/album/AbcdeF", request.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
        }

        [Fact]
        public void UpdateAlbumRequest_WithUrlNull_ThrowsArgumentNullException()
        {
            var requestBuilder = new AlbumRequestBuilder();

            var exception = Record.Exception(() => AlbumRequestBuilder.UpdateAlbumRequest(null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException) exception;
            Assert.Equal(argNullException.ParamName, "url");
        }
    }
}