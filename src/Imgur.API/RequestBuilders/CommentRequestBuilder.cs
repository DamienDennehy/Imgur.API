using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Imgur.API.Enums;

namespace Imgur.API.RequestBuilders
{
    internal class CommentRequestBuilder : RequestBuilderBase
    {
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        internal HttpRequestMessage CreateCommentRequest(string url, string comment, string imageId, string parentId)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrEmpty(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            var parameters = new Dictionary<string, string>
            {
                {"image_id", imageId},
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
        internal HttpRequestMessage CreateReplyRequest(string url, string comment, string imageId)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            if (string.IsNullOrEmpty(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            var parameters = new Dictionary<string, string>
            {
                {"image_id", imageId},
                {"comment", comment}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }

        internal HttpRequestMessage ReportCommentRequest(string url, ReportReason reason)
        {
            var parameters = new Dictionary<string, string>
            {
                {"reason", ((int) reason).ToString()}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(parameters.ToArray())
            };

            return request;
        }
    }
}