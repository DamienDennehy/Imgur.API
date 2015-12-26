using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class NotificationRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage MarkNotificationsViewedRequest(string url, IEnumerable<string> ids)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var parameters = new Dictionary<string, string>
            {
                {nameof(ids), string.Join(",", ids)}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}