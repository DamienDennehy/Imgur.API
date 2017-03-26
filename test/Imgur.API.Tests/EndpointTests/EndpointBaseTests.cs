using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class EndpointTests : TestBase
    {
        [Fact]
        public void ApiClient_SetByConstructor1_Equal()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            var endpoint = new MockEndpoint(client);
            Assert.Same(client, endpoint.ApiClient);
        }

        [Fact]
        public void ApiClient_SetByConstructor2_Equal()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            var endpoint = new MockEndpoint(client);
            Assert.Same(client, endpoint.ApiClient);
        }

        [Fact]
        public void ApiClient_SetNullByConstructor1_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MockEndpoint(null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void HttpClient_SetByConstructor1_NotNull()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            Assert.NotNull(endpoint.HttpClient);
        }

        [Fact]
        public void HttpClient_SetByConstructor2_AreSame()
        {
            var client = new ImgurClient("123", "1234");
            var httpCLient = new HttpClient();

            var endpoint = new MockEndpoint(client, httpCLient);

            Assert.Same(httpCLient, endpoint.HttpClient);
        }

        [Fact]
        public void HttpClient_SetNullByConstructor1_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MockEndpoint(new ImgurClient("test", "test"), null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void HttpClientBaseAddress_WithImgurClient_IsImgurUrl()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            Assert.Equal(new Uri("https://api.imgur.com/3/"), endpoint.HttpClient.BaseAddress);
        }

        [Fact]
        public void HttpClientBaseAddress_WithMashapeClient_IsMashapeUrl()
        {
            var client = new MashapeClient("123", "1234", "12345");
            var endpoint = new MockEndpoint(client);

            Assert.Equal(new Uri("https://imgur-apiv3.p.mashape.com/3/"), endpoint.HttpClient.BaseAddress);
        }

        [Fact]
        public void HttpClientWithImgurClient_SetByConstructor_HeadersEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.Equal("Client-ID 123", authHeader);
            Assert.Equal("application/json", accept.MediaType);
        }

        [Fact]
        public void HttpClientWithImgurClientAndOAuth2Token_SetByConstructor_HeadersEqual()
        {
            var oAuth2Token = new OAuth2Token("access_token", "refresh_token", "bearer", "11345", "bob", 2419200);
            var client = new ImgurClient("123", "1234", oAuth2Token);
            var endpoint = new MockEndpoint(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.Equal("Bearer access_token", authHeader);
            Assert.Equal("application/json", accept.MediaType);
        }

        [Fact]
        public void HttpClientWithMashapeClient_SetByConstructor_HeadersEqual()
        {
            var client = new MashapeClient("123", "1234", "1234567");
            var endpoint = new MockEndpoint(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var mashapeHeaders = endpoint.HttpClient.DefaultRequestHeaders.GetValues("X-Mashape-Key").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.Equal("Client-ID 123", authHeader);
            Assert.Equal("1234567", mashapeHeaders);
            Assert.Equal("application/json", accept.MediaType);
        }

        [Fact]
        public void HttpClientWithMashapeClientAndOAuth2Token_SetByConstructor_HeadersEqual()
        {
            var oAuth2Token = new OAuth2Token("access_token", "refresh_token", "bearer", "11345", "bob", 2419200);
            var client = new MashapeClient("123", "1234", "1234567", oAuth2Token);
            var endpoint = new MockEndpoint(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var mashapeHeaders = endpoint.HttpClient.DefaultRequestHeaders.GetValues("X-Mashape-Key").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.Equal("Bearer access_token", authHeader);
            Assert.Equal("1234567", mashapeHeaders);
            Assert.Equal("application/json", accept.MediaType);
        }

        [Fact]
        public void ProcessEndpointBaseResponse_WithResponseNull_ThrowsImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            var exception = Record.Exception(() => endpoint.ProcessEndpointResponse<bool>(null));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public void ProcessEndpointBaseResponse_WithStringResponseNull_ThrowsImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            var response = new HttpResponseMessage {Content = new StringContent("")};

            var exception = Record.Exception(() => endpoint.ProcessEndpointResponse<bool>(response));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public void ProcessEndpointResponse_WithSuccessfulResponse_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            var response = new HttpResponseMessage { Content = new StringContent(MockGenericEndpointResponses.SuccessfulResponse) };

            var result = endpoint.ProcessEndpointResponse<bool>(response);
            Assert.True(result);
        }

        [Fact]
        public void ProcessImgurEndpointResponse_WithAuthorization_ThrowImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            var response = new HttpResponseMessage {Content = new StringContent(MockErrors.ImgurClientError)};

            var exception =
                Record.Exception(() => endpoint.ProcessEndpointResponse<RateLimit>(response));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public void ProcessImgurEndpointResponse_WithExpectedCapacityError_ThrowImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            var response = new HttpResponseMessage { Content = new StringContent(MockErrors.ImgurCapacityError) };

            var exception =
                Record.Exception(() => endpoint.ProcessEndpointResponse<RateLimit>(response));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public void ProcessImgurEndpointResponse_With500Error_ThrowImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            var response = new HttpResponseMessage { Content = new StringContent(MockErrors.ImgurClient500Error) };

            var exception =
                Record.Exception(() => endpoint.ProcessEndpointResponse<RateLimit>(response));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public void ProcessImgurEndpointResponse_WithInvalidResponse_ThrowsImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            var response = new HttpResponseMessage { Content = new StringContent("<html>") };

            var exception = Record.Exception(() => endpoint.ProcessEndpointResponse<RateLimit>(response));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public void ProcessImgurEndpointResponse_WithImgurCacheErrorResponse_ThrowsImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            var response = new HttpResponseMessage {Content = new StringContent("<html>")};

            var exception = Record.Exception(() => endpoint.ProcessEndpointResponse<RateLimit>(response));
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public void ProcessMashapeEndpointResponse_WithoutAuthorization_ThrowMashapeException()
        {
            var client = new MashapeClient("123", "567567", "1234");
            var endpoint = new MockEndpoint(client);

            var response = new HttpResponseMessage { Content = new StringContent(MockErrors.MashapeError) };

            var exception = Record.Exception(() => endpoint.ProcessEndpointResponse<RateLimit>(response));
            Assert.NotNull(exception);
            Assert.IsType<MashapeException>(exception);
        }

        [Fact]
        public async Task SendRequestAsync_WithInvalidUrl__ThrowsImgurException()
        {
            var mockUrl = "http://example.org/test";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("hello world")
            };

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new MockEndpoint(new ImgurClient("123", "1234"), httpClient);

            //Query a url we know doesn't exist in the fake handler
            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test2");

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.SendRequestAsync<Image>(request).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public async Task SendRequestAsync_WithMessage_Equal()
        {
            var constructorObjects = new object[2];
            constructorObjects[0] = new ImgurClient("123", "1234");

            var mockUrl = "http://example.org/test";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.GetImage)
            };

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new MockEndpoint(new ImgurClient("123", "1234"), httpClient);

            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test");
            var image = await endpoint.SendRequestAsync<Image>(request).ConfigureAwait(false);

            Assert.NotNull(image);
        }

        [Fact]
        public async Task SendRequestAsync_WithMessageNull_ThrowsArgumentNullException()
        {
            var httpClient = new HttpClient(new MockHttpMessageHandler());
            var endpoint = new MockEndpoint(new ImgurClient("123", "1234"), httpClient);

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.SendRequestAsync<Image>(null).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public async Task SendRequestAsync_WithResponseNull_ThrowsImgurException()
        {
            var mockUrl = "http://example.org/test";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new MockEndpoint(new ImgurClient("123", "1234"), httpClient);

            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test");

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.SendRequestAsync<Image>(request).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public async Task SendRequestAsync_WithUnauthorizedErrorMessage__ThrowsImgurException()
        {
            var mockUrl = "http://example.org/test";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent(MockErrors.ImgurClientError)
            };

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new MockEndpoint(new ImgurClient("123", "1234"), httpClient);

            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test");

            var exception =
                await
                    Record.ExceptionAsync(
                        async () => await endpoint.SendRequestAsync<Image>(request).ConfigureAwait(false))
                        .ConfigureAwait(false);
            Assert.NotNull(exception);
            Assert.IsType<ImgurException>(exception);
        }

        [Fact]
        public void SwitchClient_SetImgurClient_True()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(mashapeClient);

            Assert.True(endpoint.ApiClient is IMashapeClient);
            endpoint.SwitchClient(client);
            Assert.True(endpoint.ApiClient is IImgurClient);
        }

        [Fact]
        public void SwitchClient_SetMashapeClient_True()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(client);

            Assert.True(endpoint.ApiClient is IImgurClient);
            endpoint.SwitchClient(mashapeClient);
            Assert.True(endpoint.ApiClient is IMashapeClient);
        }

        [Fact]
        public void SwitchClient_SetMashapeClientAndOAuth2TokenThenImgurClient_HeadersEqual()
        {
            var oAuth2Token = new OAuth2Token("access_token", "refresh_token", "bearer", "11345", "bob", 2419200);
            var client = new ImgurClient("123", "1234", oAuth2Token);
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(mashapeClient);

            Assert.True(endpoint.ApiClient is IMashapeClient);

            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();

            Assert.Equal("Client-ID 123444", authHeader);

            endpoint.SwitchClient(client);
            authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();

            Assert.True(endpoint.ApiClient is IImgurClient);
            Assert.Equal("Bearer access_token", authHeader);
        }

        [Fact]
        public void SwitchClient_SetMashapeClientThenImgurClient_BaseAddressEqual()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(mashapeClient);

            Assert.True(endpoint.ApiClient is IMashapeClient);

            Assert.Equal(new Uri("https://imgur-apiv3.p.mashape.com/3/"), endpoint.HttpClient.BaseAddress);

            endpoint.SwitchClient(client);

            Assert.True(endpoint.ApiClient is IImgurClient);

            Assert.Equal(new Uri("https://api.imgur.com/3/"), endpoint.HttpClient.BaseAddress);
        }

        [Fact]
        public void SwitchClient_SetMashapeClientThenImgurClient_HeadersEqual()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(mashapeClient);

            Assert.True(endpoint.ApiClient is IMashapeClient);

            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var mashapeHeaders = endpoint.HttpClient.DefaultRequestHeaders.GetValues("X-Mashape-Key").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();

            Assert.Equal("Client-ID 123444", authHeader);
            Assert.Equal("12354564", mashapeHeaders);
            Assert.Equal("application/json", accept.MediaType);

            endpoint.SwitchClient(client);
            authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();

            Assert.True(endpoint.ApiClient is IImgurClient);
            Assert.Equal("Client-ID 123", authHeader);
            Assert.Equal("application/json", accept.MediaType);
        }

        [Fact]
        public void SwitchClient_SetNull_ThrowsArgumentNullException()
        {
            var endpoint = new MockEndpoint(new ImgurClient("123", "1234"));

            var exception = Record.Exception(() => endpoint.SwitchClient(null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void UpdateRateLimit_WithHeadersNull_ThrowArgumentException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            var exception = Record.Exception(() => endpoint.UpdateRateLimit(null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void UpdateRateLimit_WithImgurClientHeaders_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            var response = new HttpResponseMessage();

            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "345");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.Equal(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.Equal(345, endpoint.ApiClient.RateLimit.ClientRemaining);

            response.Headers.Remove("X-RateLimit-ClientLimit");
            response.Headers.Remove("X-RateLimit-ClientRemaining");

            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "122");
            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "344");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.Equal(122, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.Equal(344, endpoint.ApiClient.RateLimit.ClientRemaining);
        }

        [Fact]
        public async Task UpdateRateLimit_WithImgurClientHeadersAndFakeResponse_Equal()
        {
            var mockUrl = "https://api.imgur.com/3/image/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.FavoriteImageFalse)
            };

            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "123");
            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "345");

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false);

            Assert.Equal(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.Equal(345, endpoint.ApiClient.RateLimit.ClientRemaining);

            mockResponse.Headers.Remove("X-RateLimit-ClientLimit");
            mockResponse.Headers.Remove("X-RateLimit-ClientRemaining");

            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "122");
            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "344");

            await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false);

            Assert.Equal(122, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.Equal(344, endpoint.ApiClient.RateLimit.ClientRemaining);
        }

        [Fact]
        public void UpdateRateLimit_WithImgurClientHeadersRemoved_Equal()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            var response = new HttpResponseMessage();

            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "345");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.Equal(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.Equal(345, endpoint.ApiClient.RateLimit.ClientRemaining);

            response.Headers.Remove("X-RateLimit-ClientLimit");
            response.Headers.Remove("X-RateLimit-ClientRemaining");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.Equal(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.Equal(345, endpoint.ApiClient.RateLimit.ClientRemaining);
        }

        [Fact]
        public void UpdateRateLimit_WithMashapeClientHeaders_Equal()
        {
            var client = new MashapeClient("123", "1234", "jhjhjhjh");
            var endpoint = new MockEndpoint(client);
            var response = new HttpResponseMessage();

            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Limit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Remaining", "345");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.Equal(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.Equal(345, endpoint.ApiClient.RateLimit.ClientRemaining);

            response.Headers.Remove("X-RateLimit-Requests-Limit");
            response.Headers.Remove("X-RateLimit-Requests-Remaining");

            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Limit", "122");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Remaining", "344");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.Equal(122, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.Equal(344, endpoint.ApiClient.RateLimit.ClientRemaining);
        }
    }
}