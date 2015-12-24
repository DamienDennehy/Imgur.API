using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Imgur.API.Enums;

namespace Imgur.API.RequestBuilders
{
    internal class AlbumRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage AddAlbumImagesRequest(string url, IEnumerable<string> ids)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            var parameters = new Dictionary<string, string>
            {
                {nameof(ids), string.Join(",", ids)}
            };

            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage CreateAlbumRequest(string url,
            string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null,
            string cover = null, IEnumerable<string> ids = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            var parameters = new Dictionary<string, string>();

            if (privacy != null)
                parameters.Add(nameof(privacy), $"{privacy}".ToLower());

            if (layout != null)
                parameters.Add(nameof(layout), $"{layout}".ToLower());

            if (cover != null)
                parameters.Add(nameof(cover), cover);

            if (title != null)
                parameters.Add(nameof(title), title);

            if (description != null)
                parameters.Add(nameof(description), description);

            if (ids != null)
                parameters.Add(nameof(ids), string.Join(",", ids));

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage RemoveAlbumImagesRequest(string url, IEnumerable<string> ids)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            url = $"{url}?ids={WebUtility.UrlEncode(string.Join(",", ids))}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            return request;
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage SetAlbumImagesRequest(string url, IEnumerable<string> ids)
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

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage UpdateAlbumRequest(string url,
            string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null,
            string cover = null, IEnumerable<string> ids = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            var parameters = new Dictionary<string, string>();

            if (privacy != null)
                parameters.Add(nameof(privacy), $"{privacy}".ToLower());

            if (layout != null)
                parameters.Add(nameof(layout), $"{layout}".ToLower());

            if (cover != null)
                parameters.Add(nameof(cover), cover);

            if (title != null)
                parameters.Add(nameof(title), title);

            if (description != null)
                parameters.Add(nameof(description), description);

            if (ids != null)
                parameters.Add(nameof(ids), string.Join(",", ids));

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}