using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class EndpointBaseTests
    {
        [Fact]
        public void ApiClient_SetNullByConstructor_ThrowsArgumentNullException()
        {
            var exception = Record.Exception(() =>
            {
                return new MockEndpoint(null, new HttpClient());
            });
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void ApiClient_SetByConstructor_AreSame()
        {
            var apiClient = new ApiClient("ClientId");
            var endpoint = new MockEndpoint(apiClient, new HttpClient());
            Assert.Same(apiClient, endpoint._apiClient);
        }

        [Fact]
        public void _httpClient_SetNullByConstructor1_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() =>
            {
                return new MockEndpoint(new ApiClient("test", "test"), null);
            });
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void _httpClient_SetByConstructor_AreSame()
        {
            var apiClient = new ApiClient("123", "1234");
            var httpCLient = new HttpClient();

            var endpoint = new MockEndpoint(apiClient, httpCLient);

            Assert.Same(httpCLient, endpoint._httpClient);
        }

        [Fact]
        public void _httpClient_SetByConstructor_HasBaseAddressSet()
        {
            var apiClient = new ApiClient("123", "1234");
            var endpoint = new MockEndpoint(apiClient, new HttpClient());

            Assert.Equal(new Uri("https://api.imgur.com/3/"), endpoint._httpClient.BaseAddress);
        }

        [Fact]
        public void _httpClient_SetByConstructor_HasHeadersSet()
        {
            var apiClient = new ApiClient("123", "1234");
            var endpoint = new MockEndpoint(apiClient, new HttpClient());
            var authHeader = endpoint._httpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var accept = endpoint._httpClient.DefaultRequestHeaders.Accept.First();
            Assert.Equal("Client-ID 123", authHeader);
            Assert.Equal("application/json", accept.MediaType);
        }

        [Fact]
        public async Task SendRequestAsync_WithNullMessage_ThrowsArgumentNullException()
        {
            var endpoint = new MockEndpoint(new ApiClient("123", "1234"), new HttpClient());

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.SendRequestAsync<Image>(null)
                              .ConfigureAwait(false);
            }).ConfigureAwait(false);

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);

            var argNullException = (ArgumentNullException)exception;
            Assert.Equal("httpRequestMessage", argNullException.ParamName);
        }

        [Fact]
        public async Task SendRequestAsync_WithInvalidUrl_ThrowsHttpRequestException()
        {
            var mockUrl = "http://example.org/test";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("hello world")
            };

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new MockEndpoint(new ApiClient("123", "1234"), httpClient);

            //Query a url we know doesn't exist in the fake handler
            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test2");

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.SendRequestAsync<Image>(request)
                              .ConfigureAwait(false);
            }).ConfigureAwait(false);

            Assert.NotNull(exception);
            Assert.IsType<HttpRequestException>(exception);
        }

        [Fact]
        public async Task SendRequestAsync_WithMessage_Equal()
        {
            var constructorObjects = new object[2];
            constructorObjects[0] = new ApiClient("123", "1234");

            var mockUrl = "http://example.org/test";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageResponses.GetImage)
            };

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new MockEndpoint(new ApiClient("123", "1234"), httpClient);

            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test");
            var image = await endpoint.SendRequestAsync<Image>(request)
                                      .ConfigureAwait(false);

            Assert.NotNull(image);
        }

        [Fact]
        public async Task SendRequestAsync_WithMessageNull_ThrowsArgumentNullException()
        {
            var httpClient = new HttpClient(new MockHttpMessageHandler());
            var endpoint = new MockEndpoint(new ApiClient("123", "1234"), httpClient);

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.SendRequestAsync<Image>(null).ConfigureAwait(false);
            }).ConfigureAwait(false);

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SendRequestInternalAsync_WithResponseNull_ThrowsHttpRequestException()
        {
            var mockUrl = "http://example.org/test";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new MockEndpoint(new ApiClient("123", "1234"), httpClient);

            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test");

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.SendRequestInternalAsync<Image>(request)
                              .ConfigureAwait(false);
            }).ConfigureAwait(false);

            Assert.NotNull(exception);
            Assert.IsType<HttpRequestException>(exception);
        }

        [Fact]
        public async Task SendRequestInternalAsync_WithUnauthorizedErrorMessage_ThrowsHttpRequestException()
        {
            var mockUrl = "http://example.org/test";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent(MockResponses.Error)
            };

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new MockEndpoint(new ApiClient("123", "1234"), httpClient);

            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test");

            var exception = await Record.ExceptionAsync(async () =>
            {
                await endpoint.SendRequestInternalAsync<Image>(request)
                              .ConfigureAwait(false);
            }).ConfigureAwait(false);

            Assert.NotNull(exception);
            Assert.IsType<HttpRequestException>(exception);
        }
    }
}
