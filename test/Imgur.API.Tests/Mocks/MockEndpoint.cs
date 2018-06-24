using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using System.Net.Http;

namespace Imgur.API.Tests.Mocks
{
    public class MockEndpoint : EndpointBase
    {
        public MockEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        public MockEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }
    }
}