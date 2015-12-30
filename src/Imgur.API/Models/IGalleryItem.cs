using System.Collections.Generic;

namespace Imgur.API.Models
{
    /// <summary>
    ///     Any gallery type such as GalleryImage or GalleryAlbum.
    /// </summary>
    public interface IGalleryItem : IDataModel
    {
        /// <summary>
        ///     Number of comments on the gallery item.
        /// </summary>
        int? CommentCount { get; set; }

        /// <summary>
        ///     Up to 10 top level comments, sorted by "best".
        /// </summary>
        IEnumerable<IComment> CommentPreview { get; set; }

        /// <summary>
        ///     Number of downvotes for the item.
        /// </summary>
        int? Downs { get; set; }

        /// <summary>
        ///     Upvotes minus downvotes.
        /// </summary>
        int? Points { get; set; }

        /// <summary>
        ///     Imgur popularity score.
        /// </summary>
        int? Score { get; set; }

        /// <summary>
        ///     Topic of the gallery item.
        /// </summary>
        string Topic { get; set; }

        /// <summary>
        ///     Topic ID of the gallery item.
        /// </summary>
        int? TopicId { get; set; }

        /// <summary>
        ///     Upvotes for the item.
        /// </summary>
        int? Ups { get; set; }
    }
}