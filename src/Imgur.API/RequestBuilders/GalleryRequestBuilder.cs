using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Imgur.API.Enums;

namespace Imgur.API.RequestBuilders
{
    internal class GalleryRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage PublishToGalleryRequest(string url, string title,
            string topic = null, bool? bypassTerms = null, bool? mature = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            var parameters = new Dictionary<string, string> {{nameof(title), title}};

            if (topic != null)
                parameters.Add(nameof(topic), topic);

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

        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage SearchGalleryAdvancedRequest(string url,
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

            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(qAll))
                parameters.Add("q_all", qAll);

            if (!string.IsNullOrWhiteSpace(qAny))
                parameters.Add("q_any", qAny);

            if (!string.IsNullOrWhiteSpace(qExactly))
                parameters.Add("q_exactly", qExactly);

            if (!string.IsNullOrWhiteSpace(qNot))
                parameters.Add("q_not", qNot);

            if (fileType != null)
                parameters.Add("q_type", fileType.ToString().ToLower());

            if (imageSize != null)
                parameters.Add("q_size_px", imageSize.ToString().ToLower());

            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage SearchGalleryRequest(string url, string query)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            var parameters = new Dictionary<string, string> {{"q", query}};

            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}