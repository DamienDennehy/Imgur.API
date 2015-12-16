using System;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class CommentRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage DeleteCommentRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Delete, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetCommentCountRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetCommentIdsRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetCommentRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        /// <exception cref="ArgumentNullException"></exception>
        internal HttpRequestMessage GetCommentsRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HttpRequestMessage(HttpMethod.Get, url);
        }
    }
}