using System;
using System.Collections.Generic;
using Imgur.API.Enums;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A notification of a new comment.
    /// </summary>
    public class CommentNotification : ICommentNotification
    {
        /// <summary>
        ///     The ID of the album cover image, this is what should be displayed for album comments.
        /// </summary>
        [JsonProperty("album_cover")]
        public virtual string AlbumCover { get; set; }

        /// <summary>
        ///     Username of the author of the comment.
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        ///     The account ID for the author.
        /// </summary>
        [JsonProperty("author_id")]
        public virtual int AuthorId { get; set; }

        /// <summary>
        ///     All of the replies for this comment. If there are no replies to the comment then this is an empty set.
        /// </summary>
        [JsonConverter(typeof(TypeConverter<IEnumerable<Comment>>))]
        public virtual IEnumerable<IComment> Children { get; set; } = new List<IComment>();

        /// <summary>
        ///     The comment itself.
        /// </summary>
        [JsonProperty("comment")]
        public virtual string CommentText { get; set; }

        /// <summary>
        ///     Utc timestamp of creation, converted from epoch time.
        /// </summary>
        [JsonConverter(typeof(EpochTimeConverter))]
        public virtual DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     Marked true if this caption has been deleted.
        /// </summary>
        public virtual bool Deleted { get; set; }

        /// <summary>
        ///     The number of downvotes for the comment.
        /// </summary>
        public virtual int? Downs { get; set; }

        /// <summary>
        ///     The ID for the comment.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     The Id of the image that the comment is for.
        /// </summary>
        [JsonProperty("image_id")]
        public virtual string ImageId { get; set; }

        /// <summary>
        ///     If this comment was done to an album.
        /// </summary>
        [JsonProperty("on_album")]
        public virtual bool OnAlbum { get; set; }

        /// <summary>
        ///     If this is a reply, this will be the value of the CommentId for the caption this a reply for.
        /// </summary>
        [JsonProperty("parent_id")]
        public virtual int? ParentId { get; set; }

        /// <summary>
        ///     The platform the comment was made on (Android, Desktop etc).
        /// </summary>
        public virtual string Platform { get; set; }

        /// <summary>
        ///     The number of upvotes - downvotes.
        /// </summary>
        public virtual float Points { get; set; }

        /// <summary>
        ///     Number of upvotes for the comment.
        /// </summary>
        public virtual int? Ups { get; set; }

        /// <summary>
        ///     The current user's vote on the comment. null if not signed in or if the user hasn't voted on it.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual VoteOption? Vote { get; set; }
    }
}