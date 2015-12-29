using System;
using System.Collections.Generic;
using Imgur.API.Enums;

namespace Imgur.API.Models
{
    /// <summary>
    ///     An image's comment.
    /// </summary>
    public interface IComment : IDataModel
    {
        /// <summary>
        ///     The ID of the album cover image, this is what should be displayed for album comments.
        /// </summary>
        string AlbumCover { get; set; }

        /// <summary>
        ///     Username of the author of the comment.
        /// </summary>
        string Author { get; set; }

        /// <summary>
        ///     The account ID for the author.
        /// </summary>
        int AuthorId { get; set; }

        /// <summary>
        ///     All of the replies for this comment. If there are no replies to the comment then this is an empty set.
        /// </summary>
        IEnumerable<IComment> Children { get; set; }

        /// <summary>
        ///     The comment itself.
        /// </summary>
        string CommentText { get; set; }

        /// <summary>
        ///     Utc timestamp of creation, converted from epoch time.
        /// </summary>
        DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     Marked true if this caption has been deleted.
        /// </summary>
        bool Deleted { get; set; }

        /// <summary>
        ///     The number of downvotes for the comment.
        /// </summary>
        int? Downs { get; set; }

        /// <summary>
        ///     The ID for the comment.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     The Id of the image that the comment is for.
        /// </summary>
        string ImageId { get; set; }

        /// <summary>
        ///     If this comment was done to an album.
        /// </summary>
        bool OnAlbum { get; set; }

        /// <summary>
        ///     If this is a reply, this will be the value of the CommentId for the caption this a reply for.
        /// </summary>
        int? ParentId { get; set; }

        /// <summary>
        ///     The platform the comment was made on (Android, Desktop etc).
        /// </summary>
        string Platform { get; set; }

        /// <summary>
        ///     The number of upvotes - downvotes.
        /// </summary>
        float Points { get; set; }

        /// <summary>
        ///     Number of upvotes for the comment.
        /// </summary>
        int? Ups { get; set; }

        /// <summary>
        ///     The current user's vote on the comment. null if not signed in or if the user hasn't voted on it.
        /// </summary>
        VoteOption? Vote { get; set; }
    }
}