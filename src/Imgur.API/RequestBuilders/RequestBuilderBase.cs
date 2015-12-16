using System;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal abstract class RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage CreateRequest(HttpMethod httpMethod, string url)
        {
            if (httpMethod == null)
                throw new ArgumentNullException(nameof(httpMethod));

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(httpMethod, url);
        }
    }
}