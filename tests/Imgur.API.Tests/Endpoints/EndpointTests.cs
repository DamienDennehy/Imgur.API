using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class EndpointTests
    {
        private const string ImgurAuthErrorResponse =
            "{\"data\":{\"error\":\"Authentication required\",\"request\":\"/credits\",\"method\":\"POST\"},\"success\":false,\"status\":401}";

        private const string ImgurCapacityErrorResponse =
            "{\"data\":{\"error\":\"Imgur is over capacity. Please try again.\", \"request\":\"/image/MwJTXbW\", \"method\":\"POST\" }, \"success\":false, \"status\":1203 }";

        private const string MashapeErrorResponse =
            "{\"message\":\"Missing Mashape application key.Go to http://docs.mashape.com/api-keys to learn how to get your API application key.\"}";

        private const string ImgurSimpleSuccessfulResponse = "{\"data\":true,\"success\":true,\"status\":200}";

        [TestMethod]
        public void SwitchAuthentication_WithApiAuthentication_IsReceived()
        {
            var apiAuth = Substitute.For<IApiAuthentication>();
            var endpoint = Substitute.For<IEndpoint>();
            endpoint.SwitchAuthentication(apiAuth);
            endpoint.Received().SwitchAuthentication(apiAuth);
        }

        [TestMethod]
        public void MakeEndpointRequestAsync_WithValidParameters_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IEndpoint>();
            endpoint.MakeEndpointRequestAsync<IImage>(HttpMethod.Get, "url", null);
            endpoint.Received().MakeEndpointRequestAsync<IImage>(HttpMethod.Get, "url", null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ApiAuthentication_SetNullByConstructor_ThrowArgumentNullException()
        {
            try
            {
                var constructorObjects = new object[1];
                constructorObjects[0] = null;
                var endpoint = Substitute.ForPartsOf<EndpointBase>(constructorObjects);
                endpoint.GetEndpointBaseUrl();
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        [TestMethod]
        public void ApiAuthentication_SetByConstructor_AreEqual()
        {
            var auth = new ImgurAuthentication("ClientId", "ClientSecret");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(auth);
            Assert.AreEqual(auth, endpoint.ApiAuthentication);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void GetEndpointUrl_GetWithoutSettingAuthentication_ThrowsInvalidOperationException()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            endpoint.GetEndpointBaseUrl();
        }

        [TestMethod]
        public void GetEndpointUrl_SetImgurAuthentication_AreEqual()
        {
            var auth = new ImgurAuthentication("abc", "def");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(auth);
            var endpointUrl = endpoint.GetEndpointBaseUrl();
            Assert.AreEqual("https://api.imgur.com/3/", endpointUrl);
        }

        [TestMethod]
        public void GetEndpointUrl_SetMashapeAuthentication_AreEqual()
        {
            var auth = new MashapeAuthentication("abc", "def", "hij");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(auth);
            var endpointUrl = endpoint.GetEndpointBaseUrl();
            Assert.AreEqual("https://imgur-apiv3.p.mashape.com/3/", endpointUrl);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void SwitchAuthentication_SetNull_ThrowsArgumentNullException()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            endpoint.SwitchAuthentication(null);
        }

        [TestMethod]
        public void SwitchAuthentication_SetImgurAuthentication_IsTrue()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var mashapeAuth = new MashapeAuthentication("123444", "567567", "12354564");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeAuth);
            endpoint.SwitchAuthentication(imgurAuth);

            Assert.IsTrue(endpoint.ApiAuthentication is IImgurAuthentication);
        }

        [TestMethod]
        public void SwitchAuthentication_SetMashapeAuthentication_IsTrue()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var mashapeAuth = new MashapeAuthentication("123444", "567567", "12354564");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            endpoint.SwitchAuthentication(mashapeAuth);

            Assert.IsTrue(endpoint.ApiAuthentication is IMashapeAuthentication);
        }

        [TestMethod]
        public void GetHttpClient_WithImgurAuthentication_HeadersAreEqual()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            var httpCLient = endpoint.GetHttpClient();

            var authHeaders = httpCLient.DefaultRequestHeaders.GetValues("Authorization");
            var expectedAuthValue = "Client-ID 123";

            Assert.AreEqual(expectedAuthValue.ToLower(), authHeaders.First().ToLower());
        }

        [TestMethod]
        public void GetHttpClient_WithMashapeAuthentication_HeadersAreEqual()
        {
            var mashapeAuth = new MashapeAuthentication("123", "567567", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeAuth);
            var httpCLient = endpoint.GetHttpClient();

            var authHeaders = httpCLient.DefaultRequestHeaders.GetValues("Authorization");
            var mashapeHeaders = httpCLient.DefaultRequestHeaders.GetValues("X-Mashape-Key");
            var expectedAuthValue = "Client-ID 123";
            var expectedMashapeValue = "1234";

            Assert.AreEqual(expectedAuthValue.ToLower(), authHeaders.First().ToLower());
            Assert.AreEqual(expectedMashapeValue.ToLower(), mashapeHeaders.First().ToLower());
        }

        [TestMethod]
        public void GetHttpClient_WithImgurAuthenticationAndOAuth2Authentication_HeadersAreEqual()
        {
            var token = new OAuth2Token("access_token", "refresh_token", "bearer", "11345", 3600);
            var oAuth2Auth = new OAuth2Authentication(OAuth2ResponseType.Code);
            oAuth2Auth.SetOAuth2Token(token);

            var imgurAuth = new ImgurAuthentication("123", "1234", oAuth2Auth);

            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);

            var httpCLient = endpoint.GetHttpClient();

            var authHeaders = httpCLient.DefaultRequestHeaders.GetValues("Authorization");
            var expectedAuthValue = "bearer access_token";

            var bearerValue = authHeaders.First(x => x.IndexOf("bearer", StringComparison.OrdinalIgnoreCase) >= 0);

            Assert.AreEqual(expectedAuthValue.ToLower(), bearerValue.ToLower());
        }

        [TestMethod]
        public void GetHttpClient_WithMashapeAuthenticationAndOAuth2Authentication_HeadersAreEqual()
        {
            var token = new OAuth2Token("access_token", "refresh_token", "bearer", "11345", 3600);
            var oAuth2Auth = new OAuth2Authentication(OAuth2ResponseType.Code);
            oAuth2Auth.SetOAuth2Token(token);

            var mashapeAuth = new MashapeAuthentication("123", "1234", "2322", oAuth2Auth);

            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeAuth);

            var httpCLient = endpoint.GetHttpClient();

            var authHeaders = httpCLient.DefaultRequestHeaders.GetValues("Authorization");
            var expectedAuthValue = "bearer access_token";

            var bearerValue = authHeaders.First(x => x.IndexOf("bearer", StringComparison.OrdinalIgnoreCase) >= 0);

            Assert.AreEqual(expectedAuthValue.ToLower(), bearerValue.ToLower());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ProcessEndpointBaseResponse_WithNullValue_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            endpoint.ProcessEndpointResponse<bool>(null);
        }

        [TestMethod]
        [ExpectedException(typeof (MashapeException))]
        public void ProcessMashapeEndpointResponse_WithoutAuthorization_ThrowMashapeException()
        {
            var mashapeAuth = new MashapeAuthentication("123", "567567", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeAuth);
            endpoint.ProcessEndpointResponse<RateLimit>(MashapeErrorResponse);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void ProcessImgurEndpointResponse_WithAuthorization_ThrowImgurException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            endpoint.ProcessEndpointResponse<RateLimit>(ImgurAuthErrorResponse);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void ProcessImgurEndpointResponse_WithExpectedCapacityError_ThrowImgurException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            endpoint.ProcessEndpointResponse<RateLimit>(ImgurCapacityErrorResponse);
        }

        [TestMethod]
        public void ProcessEndpointResponse_WithSuccessfulResponse_AreEqual()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            var response = endpoint.ProcessEndpointResponse<bool>(ImgurSimpleSuccessfulResponse);
            Assert.IsTrue(response);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public async Task MakeEndpointRequestAsync_WithInvalidHttpMethod_ThrowArgumentException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            await endpoint.MakeEndpointRequestAsync<RateLimit>(HttpMethod.Options, "x", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task MakeEndpointRequestAsync_WithNullHttpMethod_ThrowArgumentNullException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            await endpoint.MakeEndpointRequestAsync<RateLimit>(null, "x", null);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task MakeEndpointRequestAsync_WithNullEndpointUrl_ThrowArgumentNullException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            await endpoint.MakeEndpointRequestAsync<RateLimit>(HttpMethod.Get, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void UpdateRateLimit_WithNullHeaders_ThrowArgumentException()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);

            endpoint.UpdateRateLimit(null);
        }

        [TestMethod]
        public void UpdateRateLimit_WithImgurAuthenticationHeaders_AreEqual()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(imgurAuth);
            var response = Substitute.For<HttpResponseMessage>();

            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientLimit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-ClientRemaining", "345");
            response.Headers.TryAddWithoutValidation("X-RateLimit-UserLimit", "312");
            response.Headers.TryAddWithoutValidation("X-RateLimit-UserRemaining", "445");
            response.Headers.TryAddWithoutValidation("X-RateLimit-UserReset", "1440263353");
            endpoint.UpdateRateLimit(response.Headers);

            Assert.IsNull(endpoint.ApiAuthentication.RateLimit.MashapeUploadsLimit);
            Assert.IsNull(endpoint.ApiAuthentication.RateLimit.MashapeUploadsRemaining);
            Assert.AreEqual(123, endpoint.ApiAuthentication.RateLimit.ClientLimit);
            Assert.AreEqual(345, endpoint.ApiAuthentication.RateLimit.ClientRemaining);
            Assert.AreEqual(312, endpoint.ApiAuthentication.RateLimit.UserLimit);
            Assert.AreEqual(445, endpoint.ApiAuthentication.RateLimit.UserRemaining);

            var userReset = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1440263353);
            Assert.AreEqual(userReset, endpoint.ApiAuthentication.RateLimit.UserReset);
        }

        [TestMethod]
        public void UpdateRateLimit_WithMashapeAuthenticationHeaders_AreEqual()
        {
            var mashapeAuth = new MashapeAuthentication("123", "1234", "jhjhjhjh");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeAuth);
            var response = Substitute.For<HttpResponseMessage>();

            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Limit", "123");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Requests-Remaining", "345");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Uploads-Limit", "312");
            response.Headers.TryAddWithoutValidation("X-RateLimit-Uploads-Remaining", "445");
            endpoint.UpdateRateLimit(response.Headers);

            Assert.IsNull(endpoint.ApiAuthentication.RateLimit.UserLimit);
            Assert.IsNull(endpoint.ApiAuthentication.RateLimit.UserRemaining);
            Assert.IsNull(endpoint.ApiAuthentication.RateLimit.UserReset);
            Assert.AreEqual(123, endpoint.ApiAuthentication.RateLimit.ClientLimit);
            Assert.AreEqual(345, endpoint.ApiAuthentication.RateLimit.ClientRemaining);
            Assert.AreEqual(312, endpoint.ApiAuthentication.RateLimit.MashapeUploadsLimit);
            Assert.AreEqual(445, endpoint.ApiAuthentication.RateLimit.MashapeUploadsRemaining);
        }
    }
}