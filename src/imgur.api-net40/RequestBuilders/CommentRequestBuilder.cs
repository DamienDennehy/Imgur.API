using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class CommentRequestBuilder : RequestBuilderBase
    {
        internal static HttpRequestMessage CreateCommentRequest(string url, string comment, string galleryItemId,
            string parentId)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            var parameters = new Dictionary<string, string>
            {
                {"image_id", galleryItemId},
                {nameof(comment), comment}
            };

            if (!string.IsNullOrWhiteSpace(parentId))
                parameters.Add("parent_id", parentId);

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        internal static HttpRequestMessage CreateGalleryItemCommentRequest(string url, string comment)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            var parameters = new Dictionary<string, string>
            {
                {nameof(comment), comment}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        internal static HttpRequestMessage CreateReplyRequest(string url, string comment, string galleryItemId)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            var parameters = new Dictionary<string, string>
            {
                {"image_id", galleryItemId},
                {nameof(comment), comment}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}