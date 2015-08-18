using System.Net.Http;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class EndpointTests
    {
        [TestMethod]
        public void Endpoint_SwitchAuthentication_ReceivedIsTrue()
        {
            var apiAuth = Substitute.For<IApiAuthentication>();
            var endpoint = Substitute.For<IEndpoint>();
            endpoint.SwitchAuthentication(apiAuth);
            endpoint.Received().SwitchAuthentication(apiAuth);
        }

        [TestMethod]
        public void Endpoint_MakeEndpointRequestAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IEndpoint>();
            endpoint.MakeEndpointRequestAsync<IImage>(HttpMethod.Get, "", null);
            endpoint.Received().MakeEndpointRequestAsync<IImage>(HttpMethod.Get, "", null);
        }
    }
}
