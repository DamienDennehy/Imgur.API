using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class CustomGalleryRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage AddCustomGalleryTagsRequest(string url, IEnumerable<string> tags)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (tags == null)
                throw new ArgumentNullException(nameof(tags));

            var parameters = new Dictionary<string, string>
            {
                {nameof(tags), string.Join(",", tags)}
            };

            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage AddFilteredOutGalleryTagRequest(string url, string tag)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));

            var parameters = new Dictionary<string, string>
            {
                {nameof(tag), tag}
            };

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
        internal HttpRequestMessage RemoveCustomGalleryTagsRequest(string url, IEnumerable<string> tags)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (tags == null)
                throw new ArgumentNullException(nameof(tags));

            url = $"{url}?tags={WebUtility.UrlEncode(string.Join(",", tags))}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            return request;
        }

        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage RemoveFilteredOutGalleryTagRequest(string url, string tag)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));

            var parameters = new Dictionary<string, string>
            {
                {nameof(tag), tag}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}