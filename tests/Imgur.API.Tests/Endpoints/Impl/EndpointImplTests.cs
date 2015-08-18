using System;
using System.Linq;
using Imgur.API.Authentication;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints.Impl
{
    [TestClass]
    public class EndpointImplTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetEndpointUrl_GetWithoutSettingAuthentication_ThrowsInvalidOperationException()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            endpoint.GetEndpointUrl();
        }

        [TestMethod]
        public void GetEndpointUrl_SetImgurAuthentication_AreEqual()
        {
            var auth = new ImgurAuthentication("abc", "def");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(auth);
            var endpointUrl = endpoint.GetEndpointUrl();
            Assert.AreEqual("https://api.imgur.com/3/", endpointUrl);
        }

        [TestMethod]
        public void GetEndpointUrl_SetMashapeAuthentication_AreEqual()
        {
            var auth = new MashapeAuthentication("abc", "def");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(auth);
            var endpointUrl = endpoint.GetEndpointUrl();
            Assert.AreEqual("https://imgur-apiv3.p.mashape.com/3/", endpointUrl);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SwitchAuthentication_SetNull_ThrowsArgumentNullException()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            endpoint.SwitchAuthentication(null);
        }

        [TestMethod]
        public void SwitchAuthentication_SetImgurAuthentication_IsTrue()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var mashapeAuth = new MashapeAuthentication("123444", "12354564");
            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeAuth);
            endpoint.SwitchAuthentication(imgurAuth);

            Assert.IsTrue(endpoint.ApiAuthentication is IImgurAuthentication);
        }

        [TestMethod]
        public void SwitchAuthentication_SetMashapeAuthentication_IsTrue()
        {
            var imgurAuth = new ImgurAuthentication("123", "1234");
            var mashapeAuth = new MashapeAuthentication("123444", "12354564");
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
            var mashapeAuth = new MashapeAuthentication("123", "1234");
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

            var mashapeAuth = new MashapeAuthentication("123", "1234", oAuth2Auth);

            var endpoint = Substitute.ForPartsOf<EndpointBase>(mashapeAuth);

            var httpCLient = endpoint.GetHttpClient();

            var authHeaders = httpCLient.DefaultRequestHeaders.GetValues("Authorization");
            var expectedAuthValue = "bearer access_token";

            var bearerValue = authHeaders.First(x => x.IndexOf("bearer", StringComparison.OrdinalIgnoreCase) >= 0);

            Assert.AreEqual(expectedAuthValue.ToLower(), bearerValue.ToLower());
        }
    }
}
