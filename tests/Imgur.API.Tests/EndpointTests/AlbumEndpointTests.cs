using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class AlbumEndpointTests
    {
        [Fact]
        public async Task GetAlbumAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/album/nIn0Ntl";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumResponses.GetAlbum)
            };

            var apiClient = new ApiClient("123");
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new AlbumEndpoint(apiClient, httpClient);
            var response = await endpoint.GetAlbumAsync("nIn0Ntl");

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IAlbum>(response);
            Assert.Equal("nIn0Ntl", response.Id);
        }

        [Fact]
        public async Task GetAlbumAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123");
            var endpoint = new AlbumEndpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.GetAlbumAsync(null);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("albumId", argNullException.ParamName);
        }

        [Fact]
        public async Task GetAlbumImageAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/album/nIn0Ntl/image/nAYq66G";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockAlbumResponses.GetAlbumImage)
            };

            var apiClient = new ApiClient("123");
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new AlbumEndpoint(apiClient, httpClient);
            var response = await endpoint.GetAlbumImageAsync("nAYq66G", "nIn0Ntl");

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IImage>(response);
            Assert.Equal("nAYq66G", response.Id);
        }

        [Fact]
        public async Task GetAlbumImageAsync_WithAlbumNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123");
            var endpoint = new AlbumEndpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.GetAlbumImageAsync("image", null);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("albumId", argNullException.ParamName);
        }

        [Fact]
        public async Task GetAlbumImageAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123");
            var endpoint = new AlbumEndpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.GetAlbumImageAsync(null, "album");
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("imageId", argNullException.ParamName);
        }
    }
}
