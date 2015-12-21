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
        /// </summary>
        /// <param name="comment">The comment text, this is what will be displayed.</param>
        /// <param name="imageId">The ID of the image in the gallery that you wish to comment on.</param>
        /// <param name="parentId">The ID of the parent comment, this is an alternative method to create a reply.</param>
        /// <returns></returns>
        Task<IComment> CreateCommentAsync(string comment, string imageId, string parentId = null);

        /// <summary>
        ///     Create a reply for the given comment, returns the ID of the comment.
        /// </summary>
        /// <param name="comment">The comment text, this is what will be displayed.</param>
        /// <param name="imageId">The ID of the image in the gallery that you wish to comment on.</param>
        /// <param name="commentId">The comment id that you are replying to.</param>
        /// <returns></returns>
        Task<IComment> CreateReplyAsync(string comment, string imageId, string commentId);

        /// <summary>
        ///     Delete a comment by the given id.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <returns></returns>
        Task<bool> DeleteCommentAsync(string id);

        /// <summary>
        ///     Get information about a specific comment.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <returns></returns>
        Task<IComment> GetCommentAsync(string id);

        /// <summary>
        ///     Get the comment with all of the replies for the comment.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <returns></returns>
        Task<IComment> GetRepliesAsync(string id);

        /// <summary>
        ///     Report a comment for being inappropriate.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <param name="reason">The reason why the comment is inappropriate.</param>
        /// <returns></returns>
        Task<bool> ReportCommentAsync(string id, ReportReason reason);

        /// <summary>
        ///     Vote on a comment.
        /// </summary>
        /// <param name="id">The comment id.</param>
        /// <param name="vote">The vote.</param>
        /// <returns></returns>
        Task<bool> VoteCommentAsync(string id, Vote vote);
    }
}