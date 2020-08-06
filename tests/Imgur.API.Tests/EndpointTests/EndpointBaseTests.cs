using System;
using System.Linq;
using System.Net.Http;
using Imgur.API.Authentication;
using Imgur.API.Tests.Mocks;
using Xunit;

namespace Imgur.API.Tests.EndpointTests
{
    public class EndpointBaseTests
    {
        [Fact]
        public void ApiClient_SetNullByConstructor_ThrowsArgumentNullException()
        {
            var exception = Record.Exception(() => new MockEndpoint(null, new HttpClient()));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void ApiClient_SetByConstructor_AreSame()
        {
            var client = new ApiClient("ClientId");
            var endpoint = new MockEndpoint(client, new HttpClient());
            Assert.Same(client, endpoint.ApiClient);
        }

        [Fact]
        public void HttpClient_SetNullByConstructor1_ThrowArgumentNullException()
        {
            var exception = Record.Exception(() => new MockEndpoint(new ApiClient("test", "test"), null));
            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void HttpClient_SetByConstructor_AreSame()
        {
            var client = new ApiClient("123", "1234");
            var httpCLient = new HttpClient();

            var endpoint = new MockEndpoint(client, httpCLient);

            Assert.Same(httpCLient, endpoint.HttpClient);
        }

        [Fact]
        public void HttpClient_SetByConstructor_HasBaseAddressSet()
        {
            var client = new ApiClient("123", "1234");
            var endpoint = new MockEndpoint(client, new HttpClient());

            Assert.Equal(new Uri("https://api.imgur.com/3/"), endpoint.HttpClient.BaseAddress);
        }

        [Fact]
        public void HttpClient_SetByConstructor_HasHeadersSet()
        {
            var client = new ApiClient("123", "1234");
            var endpoint = new MockEndpoint(client, new HttpClient());
            var authHeader = endpoint.HttpClient.DefaultRequestHeaders.GetValues("Authorization").First();
            var accept = endpoint.HttpClient.DefaultRequestHeaders.Accept.First();
            Assert.Equal("Client-ID 123", authHeader);
            Assert.Equal("application/json", accept.MediaType);
        }
    }
}
