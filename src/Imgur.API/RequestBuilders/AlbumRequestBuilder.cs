using System;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class AlbumRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage DeleteAlbumRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Delete, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetAlbumCountRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetAlbumIdsRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetAlbumRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetAlbumsRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }
    }
}