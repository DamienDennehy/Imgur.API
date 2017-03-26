using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Enums;
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
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="comment">The comment text, this is what will be displayed.</param>
        /// <param name="galleryItemId">The ID of the item in the gallery that you wish to comment on.</param>
        /// <param name="parentId">The ID of the parent comment, this is an alternative method to create a reply.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<int> CreateCommentAsync(string comment, string galleryItemId, string parentId = null)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            const string url = nameof(comment);

            using (var request = CommentRequestBuilder.CreateCommentRequest(url, comment, galleryItemId, parentId))
            {
                var returnComment = await SendRequestAsync<Comment>(request).ConfigureAwait(false);
                return returnComment.Id;
            }
        }

        /// <summary>
        ///     Create a reply for the given comment, returns the ID of the comment.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="comment">The comment text, this is what will be displayed.</param>
        /// <param name="galleryItemId">The ID of the item in the gallery that you wish to comment on.</param>
        /// <param name="parentId">The comment id that you are replying to.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<int> CreateReplyAsync(string comment, string galleryItemId, string parentId)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentNullException(nameof(comment));

            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (string.IsNullOrWhiteSpace(parentId))
                throw new ArgumentNullException(nameof(parentId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"comment/{parentId}";

            using (var request = CommentRequestBuilder.CreateReplyRequest(url, comment, galleryItemId))
            {
                var returnComment = await SendRequestAsync<Comment>(request).ConfigureAwait(false);
                return returnComment.Id;
            }
        }

        /// <summary>
        ///     Delete a comment by the given id.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"comment/{commentId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Delete, url))
            {
                var deleted = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return deleted;
            }
        }

        /// <summary>
        ///     Get information about a specific comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IComment> GetCommentAsync(int commentId)
        {
            var url = $"comment/{commentId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var comment = await SendRequestAsync<Comment>(request).ConfigureAwait(false);
                return comment;
            }
        }

        /// <summary>
        ///     Get the comment with all of the replies for the comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IComment> GetRepliesAsync(int commentId)
        {
            var url = $"comment/{commentId}/replies";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var comment = await SendRequestAsync<Comment>(request).ConfigureAwait(false);
                return comment;
            }
        }

        /// <summary>
        ///     Report a comment for being inappropriate.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="reason">The reason why the comment is inappropriate.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> ReportCommentAsync(int commentId, ReportReason reason)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"comment/{commentId}/report";

            using (var request = RequestBuilderBase.ReportItemRequest(url, reason))
            {
                var reported = await SendRequestAsync<bool?>(request).ConfigureAwait(false);
                return reported ?? true;
            }
        }

        /// <summary>
        ///     Vote on a comment.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="vote">The vote.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> VoteCommentAsync(int commentId, VoteOption vote)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var voteValue = $"{vote}".ToLower();
            var url = $"comment/{commentId}/vote/{voteValue}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Post, url))
            {
                var voted = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return voted;
            }
        }
    }
}