using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        internal CommentRequestBuilder CommentRequestBuilder { get; } = new CommentRequestBuilder();

        /// <summary>
        ///     Return the comments the user has created.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="sort">The order that the comments should be sorted by. Defaults to 'newest'.</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IComment>> GetCommentsAsync(string username = "me",
            CommentSortOrder? sort = CommentSortOrder.Newest, int? page = null)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var sortValue = $"{sort ?? CommentSortOrder.Newest}".ToLower();
            var url = $"account/{username}/comments/{sortValue}/{page}";

            using (var request = CommentRequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var comments = await SendRequestAsync<IEnumerable<Comment>>(request);
                return comments;
            }
        }

        /// <summary>
        ///     Return information about a specific comment.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IComment> GetCommentAsync(string id, string username = "me")
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/comment/{id}";

            using (var request = CommentRequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var comment = await SendRequestAsync<Comment>(request);
                return comment;
            }
        }

        /// <summary>
        ///     Return a list of all of the comment IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="sort">The order that the comments should be sorted by. Defaults to 'newest'.</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetCommentIdsAsync(string username = "me",
            CommentSortOrder? sort = CommentSortOrder.Newest, int? page = null)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var sortValue = $"{sort ?? CommentSortOrder.Newest}".ToLower();
            var url = $"account/{username}/comments/ids/{sortValue}/{page}";

            using (var request = CommentRequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var comments = await SendRequestAsync<IEnumerable<string>>(request);
                return comments;
            }
        }

        /// <summary>
        ///     Return a count of all of the comments associated with the account.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<int> GetCommentCountAsync(string username = "me")
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/comments/count";

            using (var request = CommentRequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var count = await SendRequestAsync<int>(request);
                return count;
            }
        }

        /// <summary>
        ///     Delete a comment. OAuth authentication required.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> DeleteCommentAsync(string id, string username = "me")
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/comment/{id}";

            using (var request = CommentRequestBuilder.CreateRequest(HttpMethod.Delete, url))
            {
                var deleted = await SendRequestAsync<bool>(request);
                return deleted;
            }
        }
    }
}