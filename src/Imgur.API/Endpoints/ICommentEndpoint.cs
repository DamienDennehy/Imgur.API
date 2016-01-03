using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Comment related actions.
    /// </summary>
    public interface ICommentEndpoint : IEndpoint
    {
        /// <summary>
        ///     Creates a new comment, returns the ID of the comment.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="comment">The comment text, this is what will be displayed.</param>
        /// <param name="galleryItemId">The ID of the item in the gallery that you wish to comment on.</param>
        /// <param name="parentId">The ID of the parent comment, this is an alternative method to create a reply.</param>
        /// <returns></returns>
        Task<int> CreateCommentAsync(string comment, string galleryItemId, string parentId = null);

        /// <summary>
        ///     Create a reply for the given comment, returns the ID of the comment.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="comment">The comment text, this is what will be displayed.</param>
        /// <param name="galleryItemId">The ID of the item in the gallery that you wish to comment on.</param>
        /// <param name="parentId">The comment id that you are replying to.</param>
        /// <returns></returns>
        Task<int> CreateReplyAsync(string comment, string galleryItemId, string parentId);

        /// <summary>
        ///     Delete a comment by the given id.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <returns></returns>
        Task<bool> DeleteCommentAsync(int commentId);

        /// <summary>
        ///     Get information about a specific comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <returns></returns>
        Task<IComment> GetCommentAsync(int commentId);

        /// <summary>
        ///     Get the comment with all of the replies for the comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <returns></returns>
        Task<IComment> GetRepliesAsync(int commentId);

        /// <summary>
        ///     Report a comment for being inappropriate.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="reason">The reason why the comment is inappropriate.</param>
        /// <returns></returns>
        Task<bool> ReportCommentAsync(int commentId, ReportReason reason);

        /// <summary>
        ///     Vote on a comment.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="vote">The vote.</param>
        /// <returns></returns>
        Task<bool> VoteCommentAsync(int commentId, VoteOption vote);
    }
}