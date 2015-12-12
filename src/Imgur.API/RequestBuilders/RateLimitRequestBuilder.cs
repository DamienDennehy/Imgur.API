using System;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class RateLimitRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetRateLimitRequest(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }
    }
}