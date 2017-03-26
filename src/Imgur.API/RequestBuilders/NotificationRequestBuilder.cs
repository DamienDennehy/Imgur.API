using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class NotificationRequestBuilder : RequestBuilderBase
    {
        internal static HttpRequestMessage MarkNotificationsViewedRequest(string url, IEnumerable<string> notificationIds)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (notificationIds == null)
                throw new ArgumentNullException(nameof(notificationIds));

            var parameters = new Dictionary<string, string>
            {
                {"ids", string.Join(",", notificationIds)}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}