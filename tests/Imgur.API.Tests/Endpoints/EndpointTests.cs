﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.FakeResponses;
using Imgur.API.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class EndpointTests
    {
        [TestMethod]
        public void ApiClient_SetByConstructor1_AreEqual()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            Assert.AreSame(client, endpoint.ApiClient);
        }

        [TestMethod]
        public void ApiClient_SetByConstructor2_AreEqual()
        {
            var client = new ImgurClient("ClientId", "ClientSecret");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            Assert.AreSame(client, endpoint.ApiClient);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ApiClient_SetNullByConstructor1_ThrowArgumentNullException()
        {
            try
            {
                var constructorObjects = new object[1];
                constructorObjects[0] = null;
                Substitute.ForPartsOf<EndpointBase>(constructorObjects);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        public void HttpClient_SetByConstructor1_IsNotNull()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            Assert.IsNotNull(endpoint.HttpClient);
        }

        [TestMethod]
        public void HttpClient_SetByConstructor2_AreSame()
        {
            var client = new ImgurClient("123", "1234");
            var httpCLient = new HttpClient();

            var endpoint = Substitute.ForPartsOf<EndpointBase>(client, httpCLient);

            Assert.AreSame(httpCLient, endpoint.HttpClient);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void HttpClient_SetNullByConstructor1_ThrowArgumentNullException()
        {
            try
            {
                var constructorObjects = new object[2];
                constructorObjects[0] = new ImgurClient("test", "test");
                constructorObjects[1] = null;
                Substitute.ForPartsOf<EndpointBase>(constructorObjects);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        public void HttpClientBaseAddress_WithImgurClient_IsImgurUrl()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);

            Assert.AreEqual(new Uri("https://api.imgur.com/3/"), endpoint.HttpClient.BaseAddress);
        }

        [TestMethod]
        public void HttpClientBaseAddress_WithMashapeClient_IsMashapeUrl()
        {
            var client = new MashapeClient("123", "1234", "12345");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);

            Assert.AreEqual(new Uri("https://imgur-apiv3.p.mashape.com/3/"), endpoint.HttpClient.BaseAddress);
        }

        [TestMethod]
        public void HttpClientWithImgurClient_SetByConstructor_HeadersAreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
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
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.AreEqual("Bearer access_token", authHeader);
            Assert.AreEqual("application/json", accept.MediaType);
        }

        [TestMethod]
        public void HttpClientWithMashapeClient_SetByConstructor_HeadersAreEqual()
        {
            var client = new MashapeClient("123", "1234", "1234567");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
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
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var mashapeHeaders = endpoint.HttpClient.DefaultRequestHeaders.GetValues("X-Mashape-Key").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.AreEqual("Bearer access_token", authHeader);
            Assert.AreEqual("1234567", mashapeHeaders);
            Assert.AreEqual("application/json", accept.MediaType);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void ProcessEndpointBaseResponse_WithStringResponseNull_ThrowsImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            endpoint.ProcessEndpointResponse<bool>(null);
        }

        [TestMethod]
        public void ProcessEndpointResponse_WithSuccessfulResponse_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            var response = endpoint.ProcessEndpointResponse<bool>(GenericEndpointResponses.SuccessfulResponse);
            Assert.IsTrue(response);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void ProcessImgurEndpointResponse_WithAuthorization_ThrowImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            endpoint.ProcessEndpointResponse<RateLimit>(FakeErrors.ImgurClientErrorResponse);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void ProcessImgurEndpointResponse_WithExpectedCapacityError_ThrowImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            endpoint.ProcessEndpointResponse<RateLimit>(FakeErrors.ImgurCapacityErrorResponse);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void ProcessImgurEndpointResponse_WithInvalidResponse_ThrowsImgurException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            endpoint.ProcessEndpointResponse<RateLimit>("<html>");
        }

        [TestMethod]
        [ExpectedException(typeof (MashapeException))]
        public void ProcessMashapeEndpointResponse_WithoutAuthorization_ThrowMashapeException()
        {
            var client = new MashapeClient("123", "567567", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            endpoint.ProcessEndpointResponse<RateLimit>(FakeErrors.MashapeErrorResponse);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task SendRequestAsync_WithInvalidUrl_IsNull()
        {
            var constructorObjects = new object[2];
            constructorObjects[0] = new ImgurClient("123", "1234");

            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("hello world")
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("http://example.org/test"), fakeResponse);

            var httpClient = new HttpClient(fakeHttpMessageHandler);

            //Inject the fake HttpClient when declaring a new endpoint
            constructorObjects[1] = httpClient;

            var service = Substitute.ForPartsOf<EndpointBase>(constructorObjects);

            //Query a url we know doesn't exist in the fake handler
            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test2");
            var image = await service.SendRequestAsync<Image>(request);

            Assert.IsNull(image);
        }

        [TestMethod]
        public async Task SendRequestAsync_WithMessage_AreEqual()
        {
            var constructorObjects = new object[2];
            constructorObjects[0] = new ImgurClient("123", "1234");

            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(ImageEndpointResponses.Imgur.GetImageResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("http://example.org/test"), fakeResponse);

            var httpClient = new HttpClient(fakeHttpMessageHandler);

            //Inject the fake HttpClient when declaring a new endpoint
            constructorObjects[1] = httpClient;

            var service = Substitute.ForPartsOf<EndpointBase>(constructorObjects);
            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test");
            var image = await service.SendRequestAsync<Image>(request);

            Assert.IsNotNull(image);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task SendRequestAsync_WithMessageNull_ThrowsArgumentNullException()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler());
            var constructorObjects = new object[2];

            constructorObjects[0] = new ImgurClient("123", "1234");
            constructorObjects[1] = httpClient;

            var service = Substitute.ForPartsOf<EndpointBase>(constructorObjects);
            await service.SendRequestAsync<Image>(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public async Task SendRequestAsync_WithUnauthorizedErrorMessage_AreEqual()
        {
            var constructorObjects = new object[2];
            constructorObjects[0] = new ImgurClient("123", "1234");

            var fakeHttpMessageHandler = new FakeHttpMessageHandler();
            var fakeResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent(FakeErrors.ImgurClientErrorResponse)
            };

            fakeHttpMessageHandler.AddFakeResponse(new Uri("http://example.org/test"), fakeResponse);

            var httpClient = new HttpClient(fakeHttpMessageHandler);

            //Inject the fake HttpClient when declaring a new service instance
            constructorObjects[1] = httpClient;

            var service = Substitute.ForPartsOf<EndpointBase>(constructorObjects);
            var request = new HttpRequestMessage(HttpMethod.Get, "http://example.org/test");
            var image = await service.SendRequestAsync<Image>(request);

            Assert.IsNull(image);
        }

        [TestMethod]
        public void SwitchClient_SetImgurClient_IsTrue()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeClient);

            Assert.IsTrue(endpoint.ApiClient is IMashapeClient);
            endpoint.SwitchClient(client);
            Assert.IsTrue(endpoint.ApiClient is IImgurClient);
        }

        [TestMethod]
        public void SwitchClient_SetMashapeClient_IsTrue()
        {
            var client = new ImgurClient("123", "1234");
            var mashapeClient = new MashapeClient("123444", "567567", "12354564");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);

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
            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeClient);

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
            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeClient);

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
            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeClient);

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
        [ExpectedException(typeof (ArgumentNullException))]
        public void SwitchClient_SetNull_ThrowsArgumentNullException()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            endpoint.SwitchClient(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UpdateRateLimit_WithHeadersNull_ThrowArgumentException()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);

            endpoint.UpdateRateLimit(null);
        }

        [TestMethod]
        public void UpdateRateLimit_WithImgurClientHeaders_AreEqual()
        {
            var client = new ImgurClient("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            var response = Substitute.For<HttpResponseMessage>();

            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "345");
            response.Headers.TryAddWithoutValidation("X-RateLimit-UserLimit", "312");
            response.Headers.TryAddWithoutValidation("X-RateLimit-UserRemaining", "445");
            response.Headers.TryAddWithoutValidation("X-RateLimit-UserReset", "1440263353");
            endpoint.UpdateRateLimit(response.Headers);

            Assert.IsNull(endpoint.ApiClient.RateLimit.MashapeUploadsLimit);
            Assert.IsNull(endpoint.ApiClient.RateLimit.MashapeUploadsRemaining);
            Assert.AreEqual(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(345, endpoint.ApiClient.RateLimit.ClientRemaining);
            Assert.AreEqual(312, endpoint.ApiClient.RateLimit.UserLimit);
            Assert.AreEqual(445, endpoint.ApiClient.RateLimit.UserRemaining);

            var userReset = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1440263353);
            Assert.AreEqual(userReset, endpoint.ApiClient.RateLimit.UserReset);
        }

        [TestMethod]
        public void UpdateRateLimit_WithMashapeClientHeaders_AreEqual()
        {
            var client = new MashapeClient("123", "1234", "jhjhjhjh");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(client);
            var response = Substitute.For<HttpResponseMessage>();

            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Limit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Remaining", "345");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Uploads-Limit", "312");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Uploads-Remaining", "445");
            endpoint.UpdateRateLimit(response.Headers);

            Assert.IsNull(endpoint.ApiClient.RateLimit.UserLimit);
            Assert.IsNull(endpoint.ApiClient.RateLimit.UserRemaining);
            Assert.IsNull(endpoint.ApiClient.RateLimit.UserReset);
            Assert.AreEqual(123, endpoint.ApiClient.RateLimit.ClientLimit);
            Assert.AreEqual(345, endpoint.ApiClient.RateLimit.ClientRemaining);
            Assert.AreEqual(312, endpoint.ApiClient.RateLimit.MashapeUploadsLimit);
            Assert.AreEqual(445, endpoint.ApiClient.RateLimit.MashapeUploadsRemaining);
        }
    }
}