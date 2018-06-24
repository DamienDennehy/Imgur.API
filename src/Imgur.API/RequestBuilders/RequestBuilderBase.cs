using System;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal abstract class RequestBuilderBase
    {
        internal static HttpRequestMessage CreateRequest(HttpMethod httpMethod, string url)
        {
            if (httpMethod == null)
            {
                throw new ArgumentNullException(nameof(httpMethod));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            return new HttpRequestMessage(httpMethod, url);
        }
    }
}