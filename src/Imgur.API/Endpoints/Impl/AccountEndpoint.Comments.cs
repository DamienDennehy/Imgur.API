using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
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
        /// <param name="sort">'best', 'worst', 'oldest', or 'newest'. Defaults to 'newest'.</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IComment>> GetCommentsAsync(string username = "me",
            CommentSortOrder? sort = CommentSortOrder.Newest, int? page = null)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/comments/{sort}/{page}".ToLower();

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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
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
        ///     Return an array of all of the comment IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="sort">'best', 'worst', 'oldest', or 'newest'. Defaults to 'newest'.</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetCommentIdsAsync(string username = "me",
            CommentSortOrder? sort = CommentSortOrder.Newest, int? page = null)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/comments/ids/{sort}/{page}".ToLower();

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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
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
        ///     Delete a comment. You are required to be logged in as the user whom created the comment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> DeleteCommentAsync(string id, string username = "me")
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
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