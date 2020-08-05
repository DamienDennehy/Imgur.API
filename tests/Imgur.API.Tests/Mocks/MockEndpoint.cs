using System.Net.Http;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;

namespace Imgur.API.Tests.Mocks
{
    public class MockEndpoint : EndpointBase
    {
        public MockEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }
    }
}
