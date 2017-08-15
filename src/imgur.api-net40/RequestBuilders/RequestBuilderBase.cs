using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Imgur.API.Enums;

namespace Imgur.API.RequestBuilders
{
    internal abstract class RequestBuilderBase
    {
        internal static HttpRequestMessage CreateRequest(HttpMethod httpMethod, string url)
        {
            if (httpMethod == null)
                throw new ArgumentNullException(nameof(httpMethod));

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(httpMethod, url);
        }

        internal static HttpRequestMessage ReportItemRequest(string url, ReportReason reason)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            var parameters = new Dictionary<string, string>
            {
                {nameof(reason), ((int) reason).ToString()}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}