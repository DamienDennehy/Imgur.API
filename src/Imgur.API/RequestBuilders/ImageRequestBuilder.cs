using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class ImageRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage UpdateImageRequest(string url, string title = null, string description = null)
        {
            if (string.IsNullOrWhiteSpace(url))
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

        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage UploadImageBinaryRequest(string url, byte[] image, string albumId = null,
            string title = null, string description = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            // ReSharper disable once ExceptionNotDocumented
            var content = new MultipartFormDataContent($"{DateTime.UtcNow.Ticks}")
            {
                {new StringContent("file"), "type"},
                {new ByteArrayContent(image), nameof(image)}
            };

            if (!string.IsNullOrWhiteSpace(albumId))
                content.Add(new StringContent(albumId), "album");

            if (title != null)
                content.Add(new StringContent(title), nameof(title));

            if (description != null)
                content.Add(new StringContent(description), nameof(description));

            request.Content = content;

            return request;
        }

        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage UploadImageStreamRequest(string url, Stream image, string albumId = null,
            string title = null, string description = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            // ReSharper disable once ExceptionNotDocumented
            var content = new MultipartFormDataContent($"{DateTime.UtcNow.Ticks}")
            {
                {new StringContent("file"), "type"},
                {new StreamContent(image), nameof(image)}
            };

            if (!string.IsNullOrWhiteSpace(albumId))
                content.Add(new StringContent(albumId), "album");

            if (title != null)
                content.Add(new StringContent(title), nameof(title));

            if (description != null)
                content.Add(new StringContent(description), nameof(description));

            request.Content = content;

            return request;
        }

        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage UploadImageUrlRequest(string url, string imageUrl, string albumId = null,
            string title = null, string description = null)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ArgumentNullException(nameof(imageUrl));

            var parameters = new Dictionary<string, string>
            {
                {"type", "URL"},
                {"image", imageUrl}
            };

            if (!string.IsNullOrWhiteSpace(albumId))
                parameters.Add("album", albumId);

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