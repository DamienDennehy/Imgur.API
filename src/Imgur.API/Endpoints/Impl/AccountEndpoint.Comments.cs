using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        private const string GetCommentsUrl = "account/{0}/comments/{1}/{2}";
        private const string GetCommentUrl = "account/{0}/comment/{1}";
        private const string GetCommentIdsUrl = "account/{0}/comments/ids/{1}/{2}";
        private const string GetCommentCountUrl = "account/{0}/comments/count";
        private const string DeleteCommentUrl = "account/{0}/comment/{1}";


        /// <summary>
        ///     Return the comments the user has created.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="commentSortOrder">'best', 'worst', 'oldest', or 'newest'. Defaults to 'newest'.</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IComment>> GetCommentsAsync(string username = "me",
            CommentSortOrder commentSortOrder = CommentSortOrder.Newest, int? page = null)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetCommentsUrl);
            //endpointUrl = string.Format(endpointUrl, username, commentSortOrder.ToString().ToLower(), page);

            //return await MakeEndpointRequestAsync<IEnumerable<Comment>>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Return information about a specific comment.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IComment> GetCommentAsync(string id, string username = "me")
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(id))
            //    throw new ArgumentNullException(nameof(id));

            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetCommentUrl);
            //endpointUrl = string.Format(endpointUrl, username, id);
            //var comment = await MakeEndpointRequestAsync<Comment>(HttpMethod.Get, endpointUrl);
            //return comment;
        }

        /// <summary>
        ///     Return an array of all of the comment IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="commentSortOrder">'best', 'worst', 'oldest', or 'newest'. Defaults to 'newest'.</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetCommentIdsAsync(string username = "me",
            CommentSortOrder commentSortOrder = CommentSortOrder.Newest, int? page = null)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetCommentIdsUrl);
            //endpointUrl = string.Format(endpointUrl, username, commentSortOrder.ToString().ToLower(), page);

            //return await MakeEndpointRequestAsync<IEnumerable<string>>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Return a count of all of the comments associated with the account.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<int> GetCommentCountAsync(string username = "me")
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetCommentCountUrl);
            //endpointUrl = string.Format(endpointUrl, username);

            //return await MakeEndpointRequestAsync<int>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Delete a comment. You are required to be logged in as the user whom created the comment.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> DeleteCommentAsync(string id)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(id))
            //    throw new ArgumentNullException(nameof(id));

            //if (ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), DeleteCommentUrl);
            //endpointUrl = string.Format(endpointUrl, "me", id);

            //return await MakeEndpointRequestAsync<bool>(HttpMethod.Delete, endpointUrl);
        }
    }
}