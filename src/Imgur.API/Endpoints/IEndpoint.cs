using Imgur.API.Authentication;
using System.Net.Http;

namespace Imgur.API.Endpoints
{
    /// <summary>
    /// Basic Endpoint.
    /// </summary>
    public interface IEndpoint
    {
        /// <summary>
        /// The client that will be used for authentication, containing ClientId, ClientSecret, etc.
        /// </summary>
        IApiClient ApiClient { get; }

        /// <summary>
        /// HttpClient used for Http Requests.  
        /// </summary>
        HttpClient HttpClient { get; }
    }
}
