using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal static class ImageRequestBuilder
    {
        internal static HttpRequestMessage UpdateImageRequest(string url,
                                                              string title = null,
                                                              string description = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var parameters = new Dictionary<string, string>();

            if (title != null)
            {
                parameters.Add("title", title);
            }

            if (description != null)
            {
                parameters.Add("description", description);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        internal static HttpRequestMessage UploadImageStreamRequest(string url,
                                                                    Stream image,
                                                                    string album = null,
                                                                    string name = null,
                                                                    string title = null,
                                                                    string description = null,
                                                                    IProgress<int> progressBytes = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            var content = new MultipartFormDataContent($"{DateTime.UtcNow.Ticks}")
            {
                {new StringContent("file"), "type"}
            };

            if (progressBytes != null)
            {
                content.Add(new ProgressStreamContent(image, progressBytes), "image");
            }
            else
            {
                content.Add(new StreamContent(image), "image");
            }

            if (!string.IsNullOrWhiteSpace(album))
            {
                content.Add(new StringContent(album), "album");
            }

            if (name != null)
            {
                content.Add(new StringContent(name), "name");
            }

            if (title != null)
            {
                content.Add(new StringContent(title), "title");
            }

            if (description != null)
            {
                content.Add(new StringContent(description), "description");
            }

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            return request;
        }

        internal static HttpRequestMessage UploadImageUrlRequest(string url,
                                                                 string imageUrl,
                                                                 string album = null,
                                                                 string name = null,
                                                                 string title = null,
                                                                 string description = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentNullException(nameof(imageUrl));
            }

            var parameters = new Dictionary<string, string>
            {
                {"type", "URL"},
                {"image", imageUrl}
            };

            if (!string.IsNullOrWhiteSpace(album))
            {
                parameters.Add("album", album);
            }

            if (name != null)
            {
                parameters.Add("name", name);
            }

            if (title != null)
            {
                parameters.Add("title", title);
            }

            if (description != null)
            {
                parameters.Add("description", description);
            }

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}
