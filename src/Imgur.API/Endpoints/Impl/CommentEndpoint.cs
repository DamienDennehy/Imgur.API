using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Comment related actions.
    /// </summary>
    public class CommentEndpoint : EndpointBase, ICommentEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the CommentEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public CommentEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the CommentEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal CommentEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal CommentRequestBuilder RequestBuilder { get; } = new CommentRequestBuilder();

        /// <summary>
        ///     Creates a new comment, returns the ID of the comment.
        /// </summary>
        /// <param name="comment">The comment text, this is what will be displayed.</param>
        /// <param name="imageId">The ID of the image in the gallery that you wish to comment on.</param>
        /// <param name="parentId">The ID of the parent comment, this is an alternative method to create a reply.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IComment> CreateCommentAsync(string comment, string imageId, string parentId = null)
        {
            if (string.IsNullOrEmpty(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = "comment";

            using (var request = RequestBuilder.CreateCommentRequest(url, comment, imageId, parentId))
            {
                var returnComment = await SendRequestAsync<Comment>(request);
                return returnComment;
            }
        }

        /// <summary>
        ///     Create a reply for the given comment, returns the ID of the comment.
        /// </summary>
        /// <param name="comment">The comment text, this is what will be displayed.</param>
        /// <param name="imageId">The ID of the image in the gallery that you wish to comment on.</param>
        /// <param name="commentId">The comment id that you are replying to.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IComment> CreateReplyAsync(string comment, string imageId, string commentId)
        {
            if (string.IsNullOrEmpty(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrEmpty(imageId))
                throw new ArgumentNullException(nameof(imageId));

            if (string.IsNullOrEmpty(commentId))
                throw new ArgumentNullException(nameof(commentId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"comment/{commentId}";

            using (var request = RequestBuilder.CreateReplyRequest(url, comment, imageId))
            {
                var returnComment = await SendRequestAsync<Comment>(request);
                return returnComment;
            }
        }

        /// <summary>
        ///     Delete a comment by the given id.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> DeleteCommentAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"comment/{id}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Delete, url))
            {
                var deleted = await SendRequestAsync<bool>(request);
                return deleted;
            }
        }

        /// <summary>
        ///     Get information about a specific comment.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IComment> GetCommentAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var url = $"comment/{id}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var comment = await SendRequestAsync<Comment>(request);
                return comment;
            }
        }

        /// <summary>
        ///     Get the comment with all of the replies for the comment.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IComment> GetRepliesAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var url = $"comment/{id}/replies";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var comment = await SendRequestAsync<Comment>(request);
                return comment;
            }
        }

        /// <summary>
        ///     Report a comment for being inappropriate.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <param name="reason">The reason why the comment is inappropriate.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> ReportCommentAsync(string id, ReportReason reason)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"comment/{id}/report";

            using (var request = RequestBuilder.ReportCommentRequest(url, reason))
            {
                var reported = await SendRequestAsync<bool?>(request);
                return reported ?? true;
            }
        }

        /// <summary>
        ///     Vote on a comment.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <param name="vote">The vote.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> VoteCommentAsync(string id, Vote vote)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var voteValue = $"{vote}".ToLower();
            var url = $"comment/{id}/vote/{voteValue}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Post, url))
            {
                var voted = await SendRequestAsync<bool>(request);
                return voted;
            }
        }
    }
}