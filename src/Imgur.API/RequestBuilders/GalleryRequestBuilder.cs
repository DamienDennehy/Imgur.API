using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Imgur.API.Enums;

namespace Imgur.API.RequestBuilders
{
    internal class GalleryRequestBuilder : RequestBuilderBase
    {
        internal static HttpRequestMessage PublishToGalleryRequest(string url, string title,
            string topicId = null, bool? bypassTerms = null, bool? mature = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            var parameters = new Dictionary<string, string> {{nameof(title), title}};

            if (topicId != null)
                parameters.Add("topic", topicId);

            if (bypassTerms != null)
                parameters.Add("terms", $"{bypassTerms}".ToLower());

            if (mature != null)
                parameters.Add(nameof(mature), $"{mature}".ToLower());

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        internal static string SearchGalleryAdvancedRequest(string url,
            string qAll = null, string qAny = null,
            string qExactly = null, string qNot = null,
            ImageFileType? fileType = null, ImageSize? imageSize = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(qAll) &&
                string.IsNullOrWhiteSpace(qAny) &&
                string.IsNullOrWhiteSpace(qExactly) &&
                string.IsNullOrWhiteSpace(qNot))
                throw new ArgumentNullException(null,
                    "At least one search parameter must be provided (All | Any | Exactly | Not).");

            var query = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(qAll))
                query.Append($"&q_all={WebUtility.UrlEncode(qAll)}");

            if (!string.IsNullOrWhiteSpace(qAny))
                query.Append($"&q_any={WebUtility.UrlEncode(qAny)}");

            if (!string.IsNullOrWhiteSpace(qExactly))
                query.Append($"&q_exactly={WebUtility.UrlEncode(qExactly)}");

            if (!string.IsNullOrWhiteSpace(qNot))
                query.Append($"&q_not={WebUtility.UrlEncode(qNot)}");

            if (fileType != null)
                query.Append($"&q_type={WebUtility.UrlEncode(fileType.ToString().ToLower())}");

            if (imageSize != null)
                query.Append($"&q_size_px={WebUtility.UrlEncode(imageSize.ToString().ToLower())}");

            return $"{url}?{query}".Replace("?&", "?");
        }

        internal static string SearchGalleryRequest(string url, string query)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            return $"{url}?q={WebUtility.UrlEncode(query)}";
        }
    }
}