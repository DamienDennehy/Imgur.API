using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Gets credit limit.
    /// </summary>
    public class RateLimitEndpoint : EndpointBase, IRateLimitEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the RateLimitEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public RateLimitEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the RateLimitEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal RateLimitEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal RateLimitRequestBuilder RequestBuilder { get; } = new RateLimitRequestBuilder();

        /// <summary>
        ///     Gets remaining credits for the application.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <returns></returns>
        public async Task<IRateLimit> GetRateLimitAsync()
        {
            IRateLimit limit;

            var url = "credits";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                limit = await SendRequestAsync<RateLimit>(request).ConfigureAwait(false);
            }

            return limit;
        }
    }
}