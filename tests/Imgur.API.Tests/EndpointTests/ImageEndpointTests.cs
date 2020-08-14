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

            var apiClient = new ApiClient("123");
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
            var apiClient = new ApiClient("123");
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
            var mockUrl = "https://api.imgur.com/3/upload";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageResponses.UploadImage)
            };

            var apiClient = new ApiClient("123");
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new ImageEndpoint(apiClient, httpClient);

            using var ms = new MemoryStream(new byte[9]);
            var response = await endpoint.UploadImageAsync(ms);

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IImage>(response);
            Assert.Equal("mvWNMH4", response.Id);
        }

        [Fact]
        public async Task UploadImageAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123");
            var endpoint = new ImageEndpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.UploadImageAsync(image: null);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("image", argNullException.ParamName);
        }

        [Fact]
        public async Task UploadVideoAsync_WithImageNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123");
            var endpoint = new ImageEndpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.UploadVideoAsync(video: null);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("video", argNullException.ParamName);
        }

        [Fact]
        public async Task UploadVideoAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/upload";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageResponses.UploadImage)
            };

            var apiClient = new ApiClient("123");
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new ImageEndpoint(apiClient, httpClient);

            using var ms = new MemoryStream(new byte[9]);
            var response = await endpoint.UploadVideoAsync(ms);

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IImage>(response);
            Assert.Equal("mvWNMH4", response.Id);
        }

        [Fact]
        public async Task UploadImageUrlAsync_WithImageIrlNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123");
            var endpoint = new ImageEndpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.UploadImageAsync(imageUrl: null);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("imageUrl", argNullException.ParamName);
        }

        [Fact]
        public async Task UploadImageUrlAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageResponses.UploadImage)
            };

            var apiClient = new ApiClient("123");
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new ImageEndpoint(apiClient, httpClient);

            var response = await endpoint.UploadImageAsync(mockUrl);

            Assert.NotNull(response);
            Assert.IsAssignableFrom<IImage>(response);
            Assert.Equal("mvWNMH4", response.Id);
        }

        [Fact]
        public async Task DeleteImageAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image/123xyj";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageResponses.DeleteImage)
            };

            var apiClient = new ApiClient("123");
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new ImageEndpoint(apiClient, httpClient);

            var deleted = await endpoint.DeleteImageAsync("123xyj");

            Assert.True(deleted);
        }

        [Fact]
        public async Task DeleteImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123");
            var httpClient = new HttpClient();
            var endpoint = new ImageEndpoint(apiClient, httpClient);

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.DeleteImageAsync(null);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("imageId", argNullException.ParamName);
        }

        [Fact]
        public async Task FavoriteImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var apiClient = new ApiClient("123");
            var endpoint = new ImageEndpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.FavoriteImageAsync(null)
                                                                                                  .ConfigureAwait(false);
            }).ConfigureAwait(false);

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("imageId", argNullException.ParamName);
        }

        [Fact]
        public async Task FavoriteImageAsync_WithOAuth2TokenNotSet_ThrowsInvalidOperationException()
        {
            var apiClient = new ApiClient("123");
            var endpoint = new ImageEndpoint(apiClient, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.FavoriteImageAsync("abc")
                                                                                                  .ConfigureAwait(false);
            }).ConfigureAwait(false);

            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async Task FavoriteImageAsync_WithApiClient_ReturnsFavorited()
        {
            var mockUrl = "https://api.imgur.com/3/image/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageResponses.FavoriteImage)
            };

            var apiClient = new ApiClient("123");
            apiClient.SetOAuth2Token(MockOAuth2Token.GetOAuth2Token());
            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new ImageEndpoint(apiClient, httpClient);
            var favorited = await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false);

            Assert.NotNull(favorited);
        }


        [Fact]
        public async Task UpdateImageAsync_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image/123xyj";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageResponses.UpdateImage)
            };

            var client = new ApiClient("123");
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));
            var updated = await endpoint.UpdateImageAsync("123xyj").ConfigureAwait(false);

            Assert.True(updated);
        }

        [Fact]
        public async Task UpdateImageAsync_WithIdNull_ThrowsArgumentNullException()
        {
            var client = new ApiClient("123");
            var endpoint = new ImageEndpoint(client, new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.UpdateImageAsync(null).ConfigureAwait(false);
            }).ConfigureAwait(false);

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
