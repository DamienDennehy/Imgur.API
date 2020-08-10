using System;
using System.IO;
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
    public class ImageEndpointTests
    {
        [Fact]
        public async Task GetImageAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image/mvWNMH4";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageResponses.GetImage)
            };

            var apiClient = new ApiClient("123", "1234");
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new ImageEndpoint(apiClient, httpClient);
            var response = await endpoint.GetImageAsync("mvWNMH4");

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IImage>(response);
            Assert.Equal("mvWNMH4", response.Id);
        }

        [Fact]
        public async Task GetImageAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123", "1234");
            var endpoint = new ImageEndpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.GetImageAsync(null);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("imageId", argNullException.ParamName);
        }

        [Fact]
        public async Task UploadImageAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageResponses.UploadImage)
            };

            var apiClient = new ApiClient("123", "1234");
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new ImageEndpoint(apiClient, httpClient);

            using var ms = new MemoryStream(new byte[9]);
            var response = await endpoint.UploadImageAsync(ms);

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IImage>(response);
            Assert.Equal("mvWNMH4", response.Id);
        }
    }
}
