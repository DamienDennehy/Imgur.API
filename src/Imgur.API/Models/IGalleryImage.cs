using System.Collections.Generic;

namespace Imgur.API.Models
{
    /// <summary>
    ///     The data model formatted for gallery images.
    /// </summary>
    public interface IGalleryImage : IGalleryItem, IImage
    {
        /// <summary>
        ///     The account ID for the uploader, or null.
        /// </summary>
        string AccountId { get; set; }

        /// <summary>
        ///     The username of the account that uploaded it, or null.
        /// </summary>
        string AccountUrl { get; set; }

        /// <summary>
        ///     Number of comments on the gallery image.
        /// </summary>
        int? CommentCount { get; set; }

        /// <summary>
        ///     Up to 10 top level comments, sorted by "best".
        /// </summary>
        IEnumerable<IComment> CommentPreview { get; set; }

        /// <summary>
        ///     Number of downvotes for the image.
        /// </summary>
        int? Downs { get; set; }

        /// <summary>
        ///     Imgur popularity score.
        /// </summary>
        int? Score { get; set; }

        /// <summary>
        ///     Topic of the gallery image.
        /// </summary>
        string Topic { get; set; }

        /// <summary>
        ///     Topic ID of the gallery image.
        /// </summary>
        int? TopicId { get; set; }

        /// <summary>
        ///     Upvotes for the image.
        /// </summary>
        int? Ups { get; set; }
    }
}