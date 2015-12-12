using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class ImageRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage DeleteImageRequest(string url, string id)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            return new HttpRequestMessage(HttpMethod.Delete, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage FavoriteImageRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            return request;
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetImageRequest(string url, string id)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage UpdateImageRequest(string url, string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(title))
                parameters.Add(nameof(title), title);

            if (!string.IsNullOrWhiteSpace(description))
                parameters.Add(nameof(description), description);

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage UploadImageBinaryRequest(string url, byte[] image, string album = null,
            string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = new MultipartFormDataContent(DateTime.UtcNow.Ticks.ToString())
            {
                {new StringContent("file"), "type"},
                {new ByteArrayContent(image), nameof(image)}
            };

            if (!string.IsNullOrWhiteSpace(album))
                content.Add(new StringContent(album), nameof(album));

            if (!string.IsNullOrWhiteSpace(title))
                content.Add(new StringContent(title), nameof(title));

            if (!string.IsNullOrWhiteSpace(description))
                content.Add(new StringContent(description), nameof(description));

            request.Content = content;

            return request;
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage UploadImageStreamRequest(string url, Stream image, string album = null,
            string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var content = new MultipartFormDataContent(DateTime.UtcNow.Ticks.ToString())
            {
                {new StringContent("file"), "type"},
                {new StreamContent(image), nameof(image)}
            };

            if (!string.IsNullOrWhiteSpace(album))
                content.Add(new StringContent(album), nameof(album));

            if (!string.IsNullOrWhiteSpace(title))
                content.Add(new StringContent(title), nameof(title));

            if (!string.IsNullOrWhiteSpace(description))
                content.Add(new StringContent(description), nameof(description));

            request.Content = content;

            return request;
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage UploadImageUrlRequest(string url, string image, string album = null,
            string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrEmpty(image))
                throw new ArgumentNullException(nameof(image));

            var parameters = new Dictionary<string, string>
            {
                {"type", "URL"},
                {"image", image}
            };

            if (!string.IsNullOrWhiteSpace(album))
                parameters.Add(nameof(album), album);

            if (!string.IsNullOrWhiteSpace(title))
                parameters.Add(nameof(title), title);

            if (!string.IsNullOrWhiteSpace(description))
                parameters.Add(nameof(description), description);

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}