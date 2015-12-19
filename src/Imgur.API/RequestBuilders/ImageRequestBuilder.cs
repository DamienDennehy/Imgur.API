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
        internal HttpRequestMessage UpdateImageRequest(string url, string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            var parameters = new Dictionary<string, string>();

            if (title != null)
                parameters.Add(nameof(title), title);

            if (description != null)
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

            var content = new MultipartFormDataContent($"{DateTime.UtcNow.Ticks}")
            {
                {new StringContent("file"), "type"},
                {new ByteArrayContent(image), nameof(image)}
            };

            if (album != null)
                content.Add(new StringContent(album), nameof(album));

            if (title != null)
                content.Add(new StringContent(title), nameof(title));

            if (description != null)
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

            var content = new MultipartFormDataContent($"{DateTime.UtcNow.Ticks}")
            {
                {new StringContent("file"), "type"},
                {new StreamContent(image), nameof(image)}
            };

            if (album != null)
                content.Add(new StringContent(album), nameof(album));

            if (title != null)
                content.Add(new StringContent(title), nameof(title));

            if (description != null)
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

            if (album != null)
                parameters.Add(nameof(album), album);

            if (title != null)
                parameters.Add(nameof(title), title);

            if (description != null)
                parameters.Add(nameof(description), description);

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}