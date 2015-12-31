using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Imgur.API.RequestBuilders
{
    internal class CommentRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage CreateCommentRequest(string url, string comment, string galleryItemId,
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
                {"comment", comment}
            };

            if (!string.IsNullOrWhiteSpace(parentId))
                parameters.Add("parent_id", parentId);

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
        internal HttpRequestMessage CreateGalleryItemCommentRequest(string url, string comment)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            var parameters = new Dictionary<string, string>
            {
                {"comment", comment}
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
        internal HttpRequestMessage CreateReplyRequest(string url, string comment, string galleryItemId)
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
                {"comment", comment}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}