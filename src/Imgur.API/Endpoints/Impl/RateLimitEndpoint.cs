using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Exceptions;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Gets credit limit.
    /// </summary>
    public class RateLimitEndpoint : EndpointBase, IRateLimitEndpoint
    {
        private const string RateLimitUrl = "credits";

        /// <summary>
        ///     Initializes a new instance of the RateLimitEndpoint class.
        /// </summary>
        /// <param name="authentication"></param>
        public RateLimitEndpoint(IApiClient authentication) : base(authentication)
        {
        }

        /// <summary>
        ///     Get RateLimit.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IRateLimit> GetRateLimitAsync()
        {
            var endpointUrl = string.Concat(GetEndpointBaseUrl(), RateLimitUrl);
            var limit = await MakeEndpointRequestAsync<RateLimit>(HttpMethod.Get, endpointUrl, null);
            return limit;
        }
    }
}