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
        internal static HttpRequestMessage AddAlbumImagesRequest(string url, IEnumerable<string> imageIds)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (imageIds == null)
                throw new ArgumentNullException(nameof(imageIds));

            var parameters = new Dictionary<string, string>
            {
                {"ids", string.Join(",", imageIds)}
            };

            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        internal static HttpRequestMessage CreateAlbumRequest(string url,
            string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null,
            string coverId = null, IEnumerable<string> imageIds = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            var parameters = new Dictionary<string, string>();

            if (privacy != null)
                parameters.Add(nameof(privacy), $"{privacy}".ToLower());

            if (layout != null)
                parameters.Add(nameof(layout), $"{layout}".ToLower());

            if (coverId != null)
                parameters.Add("cover", coverId);

            if (title != null)
                parameters.Add(nameof(title), title);

            if (description != null)
                parameters.Add(nameof(description), description);

            if (imageIds != null)
                parameters.Add("ids", string.Join(",", imageIds));

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        internal static HttpRequestMessage RemoveAlbumImagesRequest(string url, IEnumerable<string> imageIds)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (imageIds == null)
                throw new ArgumentNullException(nameof(imageIds));

            url = $"{url}?ids={WebUtility.UrlEncode(string.Join(",", imageIds))}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            return request;
        }

        internal static HttpRequestMessage SetAlbumImagesRequest(string url, IEnumerable<string> imageIds)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (imageIds == null)
                throw new ArgumentNullException(nameof(imageIds));

            var parameters = new Dictionary<string, string>
            {
                {"ids", string.Join(",", imageIds)}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        internal static HttpRequestMessage UpdateAlbumRequest(string url,
            string title = null, string description = null,
            AlbumPrivacy? privacy = null, AlbumLayout? layout = null,
            string coverId = null, IEnumerable<string> imageIds = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            var parameters = new Dictionary<string, string>();

            if (privacy != null)
                parameters.Add(nameof(privacy), $"{privacy}".ToLower());

            if (layout != null)
                parameters.Add(nameof(layout), $"{layout}".ToLower());

            if (coverId != null)
                parameters.Add("cover", coverId);

            if (title != null)
                parameters.Add(nameof(title), title);

            if (description != null)
                parameters.Add(nameof(description), description);

            if (imageIds != null)
                parameters.Add("ids", string.Join(",", imageIds));

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}