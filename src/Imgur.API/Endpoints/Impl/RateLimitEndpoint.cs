using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
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
        public RateLimitEndpoint(IApiAuthentication authentication) : base(authentication)
        {
        }

        /// <summary>
        ///     Get RateLimit.
        /// </summary>
        /// <returns></returns>
        public async Task<IRateLimit> GetRateLimitAsync()
        {
            var endpointUrl = string.Concat(GetEndpointBaseUrl(), RateLimitUrl);
            var limit = await MakeEndpointRequestAsync<RateLimit>(HttpMethod.Get, endpointUrl, null);
            return limit;
        }
    }
}