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
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable ExceptionNotDocumented
// ReSharper disable ThrowingSystemException

namespace Imgur.API.Tests.EndpointTests
{
    [TestClass]
    public class EndpointTests : TestBase
    {
        [TestMethod]
        public void ApiClient_SetByConstructor1_AreEqual()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            var endpoint = new MockEndpoint(client);
            Assert.AreSame(client, endpoint.ApiClient);
        }

        [TestMethod]
        public void ApiClient_SetByConstructor2_AreEqual()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            var endpoint = new MockEndpoint(client);
            Assert.AreSame(client, endpoint.ApiClient);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApiClient_SetNullByConstructor1_ThrowArgumentNullException()
        {
            new MockEndpoint(null);
        }

        [TestMethod]
        public void HttpClient_SetByConstructor1_IsNotNull()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            Assert.IsNotNull(endpoint.HttpClient);
        }

        [TestMethod]
        public void HttpClient_SetByConstructor2_AreSame()
        {
            var client = new ImgurClient("123", "1234");
            var httpCLient = new HttpClient();

            var endpoint = new MockEndpoint(client, httpCLient);

            Assert.AreSame(httpCLient, endpoint.HttpClient);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpClient_SetNullByConstructor1_ThrowArgumentNullException()
        {
            new MockEndpoint(new ImgurClient("test", "test"), null);
        }

        [TestMethod]
        public void HttpClientBaseAddress_WithImgurClient_IsImgurUrl()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            Assert.AreEqual(new Uri("https://api.imgur.com/3/"), endpoint.HttpClient.BaseAddress);
        }

        [TestMethod]
        public void HttpClientBaseAddress_WithMashapeClient_IsMashapeUrl()
        {
            var client = new MashapeClient("123", "1234", "12345");
            var endpoint = new MockEndpoint(client);

            Assert.AreEqual(new Uri("https://imgur-apiv3.p.mashape.com/3/"), endpoint.HttpClient.BaseAddress);
        }

        [TestMethod]
        public void HttpClientWithImgurClient_SetByConstructor_HeadersAreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.AreEqual("Client-ID 123", authHeader);
            Assert.AreEqual("application/json", accept.MediaType);
        }

        [TestMethod]
        public void HttpClientWithImgurClientAndOAuth2Token_SetByConstructor_HeadersAreEqual()
        {
            var oAuth2Token = new OAuth2Token("access_token", "refresh_token", "bearer", "11345", "bob", 2419200);
            var client = new ImgurClient("123", "1234", oAuth2Token);
            var endpoint = new MockEndpoint(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.AreEqual("Bearer access_token", authHeader);
            Assert.AreEqual("application/json", accept.MediaType);
        }

        [TestMethod]
        public void HttpClientWithMashapeClient_SetByConstructor_HeadersAreEqual()
        {
            var client = new MashapeClient("123", "1234", "1234567");
            var endpoint = new MockEndpoint(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var mashapeHeaders = endpoint.HttpClient.DefaultRequestHeaders.GetValues("X-Mashape-Key").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.AreEqual("Client-ID 123", authHeader);
            Assert.AreEqual("1234567", mashapeHeaders);
            Assert.AreEqual("application/json", accept.MediaType);
        }

        [TestMethod]
        public void HttpClientWithMashapeClientAndOAuth2Token_SetByConstructor_HeadersAreEqual()
        {
            var oAuth2Token = new OAuth2Token("access_token", "refresh_token", "bearer", "11345", "bob", 2419200);
            var client = new MashapeClient("123", "1234", "1234567", oAuth2Token);
            var endpoint = new MockEndpoint(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var mashapeHeaders = endpoint.HttpClient.DefaultRequestHeaders.GetValues("X-Mashape-Key").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.AreEqual("Bearer access_token", authHeader);
            Assert.AreEqual("1234567", mashapeHeaders);
            Assert.AreEqual("application/json", accept.MediaType);
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
        public void ProcessEndpointBaseResponse_WithStringResponseNull_ThrowsImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            endpoint.ProcessEndpointResponse<bool>(null);
        }

        [TestMethod]
        public void ProcessEndpointResponse_WithSuccessfulResponse_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            var response = endpoint.ProcessEndpointResponse<bool>(MockGenericEndpointResponses.SuccessfulResponse);
            Assert.IsTrue(response);
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
        public void ProcessImgurEndpointResponse_WithAuthorization_ThrowImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            endpoint.ProcessEndpointResponse<RateLimit>(MockErrors.ImgurClientError);
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
        public void ProcessImgurEndpointResponse_WithExpectedCapacityError_ThrowImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            endpoint.ProcessEndpointResponse<RateLimit>(MockErrors.ImgurCapacityError);
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
        public void ProcessImgurEndpointResponse_WithInvalidResponse_ThrowsImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            endpoint.ProcessEndpointResponse<RateLimit>("<html>");
        }

        [TestMethod]
        [ExpectedException(typeof(MashapeException))]
        public void ProcessMashapeEndpointResponse_WithoutAuthorization_ThrowMashapeException()
        {
            var client = new MashapeClient("123", "567567", "1234");
            var endpoint = new MockEndpoint(client);
            endpoint.ProcessEndpointResponse<RateLimit>(MockErrors.MashapeError);
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
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
            var image = await endpoint.SendRequestAsync<Image>(request).ConfigureAwait(false);

            Assert.IsNull(image);
        }

        [TestMethod]
        public async Task SendRequestAsync_WithMessage_AreEqual()
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

            Assert.IsNotNull(image);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task SendRequestAsync_WithMessageNull_ThrowsArgumentNullException()
        {
            var httpClient = new HttpClient(new MockHttpMessageHandler());
            var endpoint = new MockEndpoint(new ImgurClient("123", "1234"), httpClient);
            await endpoint.SendRequestAsync<Image>(null).ConfigureAwait(false);
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
        public async Task SendRequestAsync_WithResponseNull_ThrowsImgurException()
        {
            var mockUrl = "http://example.org/test";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            var httpClient = new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse));
            var endpoint = new MockEndpoint(new ImgurClient("123", "1234"), httpClient);

            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test");
            var image = await endpoint.SendRequestAsync<Image>(request).ConfigureAwait(false);

            Assert.IsNull(image);
        }

        [TestMethod]
        [ExpectedException(typeof(ImgurException))]
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
            var image = await endpoint.SendRequestAsync<Image>(request).ConfigureAwait(false);

            Assert.IsNull(image);
        }

        [TestMethod]
        public void SwitchClient_SetImgurClient_IsTrue()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(mashapeClient);

            Assert.IsTrue(endpoint.ApiClient is IMashapeClient);
            endpoint.SwitchClient(client);
            Assert.IsTrue(endpoint.ApiClient is IImgurClient);
        }

        [TestMethod]
        public void SwitchClient_SetMashapeClient_IsTrue()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(client);

            Assert.IsTrue(endpoint.ApiClient is IImgurClient);
            endpoint.SwitchClient(mashapeClient);
            Assert.IsTrue(endpoint.ApiClient is IMashapeClient);
        }

        [TestMethod]
        public void SwitchClient_SetMashapeClientAndOAuth2TokenThenImgurClient_HeadersAreEqual()
        {
            var oAuth2Token = new OAuth2Token("access_token", "refresh_token", "bearer", "11345", "bob", 2419200);
            var client = new ImgurClient("123", "1234", oAuth2Token);
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(mashapeClient);

            Assert.IsTrue(endpoint.ApiClient is IMashapeClient);

            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();

            Assert.AreEqual("Client-ID 123444", authHeader);

            endpoint.SwitchClient(client);
            authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();

            Assert.IsTrue(endpoint.ApiClient is IImgurClient);
            Assert.AreEqual("Bearer access_token", authHeader);
        }

        [TestMethod]
        public void SwitchClient_SetMashapeClientThenImgurClient_BaseAddressAreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(mashapeClient);

            Assert.IsTrue(endpoint.ApiClient is IMashapeClient);

            Assert.AreEqual(new Uri("https://imgur-apiv3.p.mashape.com/3/"), endpoint.HttpClient.BaseAddress);

            endpoint.SwitchClient(client);

            Assert.IsTrue(endpoint.ApiClient is IImgurClient);

            Assert.AreEqual(new Uri("https://api.imgur.com/3/"), endpoint.HttpClient.BaseAddress);
        }

        [TestMethod]
        public void SwitchClient_SetMashapeClientThenImgurClient_HeadersAreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = new MockEndpoint(mashapeClient);

            Assert.IsTrue(endpoint.ApiClient is IMashapeClient);

            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var mashapeHeaders = endpoint.HttpClient.DefaultRequestHeaders.GetValues("X-Mashape-Key").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();

            Assert.AreEqual("Client-ID 123444", authHeader);
            Assert.AreEqual("12354564", mashapeHeaders);
            Assert.AreEqual("application/json", accept.MediaType);

            endpoint.SwitchClient(client);
            authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();

            Assert.IsTrue(endpoint.ApiClient is IImgurClient);
            Assert.AreEqual("Client-ID 123", authHeader);
            Assert.AreEqual("application/json", accept.MediaType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SwitchClient_SetNull_ThrowsArgumentNullException()
        {
            var endpoint = new MockEndpoint(new ImgurClient("123", "1234"));
            endpoint.SwitchClient(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateRateLimit_WithHeadersNull_ThrowArgumentException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);

            endpoint.UpdateRateLimit(null);
        }

        [TestMethod]
        public void UpdateRateLimit_WithImgurClientHeaders_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            var response = new HttpResponseMessage();

            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "345");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.AreEqual(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(345, endpoint.ApiClient.RateLimit.ClientRemaining);

            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "122");
            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "344");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.AreEqual(122, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(344, endpoint.ApiClient.RateLimit.ClientRemaining);
        }

        [TestMethod]
        public async Task UpdateRateLimit_WithImgurClientHeadersAndFakeResponse_AreEqual()
        {
            var mockUrl = "https://api.imgur.com/3/image/zVpyzhW/favorite";
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(MockImageEndpointResponses.Imgur.FavoriteImageAsyncFalse)
            };

            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "123");
            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "345");

            var client = new ImgurClient("123", "1234", MockOAuth2Token);
            var endpoint = new ImageEndpoint(client, new HttpClient(new MockHttpMessageHandler(mockUrl, mockResponse)));

            await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false);

            Assert.AreEqual(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(345, endpoint.ApiClient.RateLimit.ClientRemaining);

            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "122");
            mockResponse.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "344");

            await endpoint.FavoriteImageAsync("zVpyzhW").ConfigureAwait(false);

            Assert.AreEqual(122, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(344, endpoint.ApiClient.RateLimit.ClientRemaining);
        }

        [TestMethod]
        public void UpdateRateLimit_WithImgurClientHeadersRemoved_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = new MockEndpoint(client);
            var response = new HttpResponseMessage();

            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "345");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.AreEqual(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(345, endpoint.ApiClient.RateLimit.ClientRemaining);

            response.Headers.Remove("X-RateLimit-ClientLimit");
            response.Headers.Remove("X-RateLimit-ClientRemaining");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.AreEqual(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(345, endpoint.ApiClient.RateLimit.ClientRemaining);
        }

        [TestMethod]
        public void UpdateRateLimit_WithMashapeClientHeaders_AreEqual()
        {
            var client = new MashapeClient("123", "1234", "jhjhjhjh");
            var endpoint = new MockEndpoint(client);
            var response = new HttpResponseMessage();

            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Limit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Remaining", "345");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.AreEqual(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(345, endpoint.ApiClient.RateLimit.ClientRemaining);

            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Limit", "122");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Remaining", "344");

            endpoint.UpdateRateLimit(response.Headers);

            Assert.AreEqual(122, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(344, endpoint.ApiClient.RateLimit.ClientRemaining);
        }
    }
}