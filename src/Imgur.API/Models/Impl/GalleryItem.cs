using System.Collections.Generic;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     Any gallery type such as GalleryImage or GalleryAlbum.
    /// </summary>
    public class GalleryItem : IGalleryItem
    {
        /// <summary>
        ///     Number of comments on the gallery item.
        /// </summary>
        [JsonProperty("comment_count")]
        public int? CommentCount { get; set; }

        /// <summary>
        ///     Up to 10 top level comments, sorted by "best".
        /// </summary>
        [JsonProperty("comment_preview")]
        [JsonConverter(typeof (TypeConverter<IEnumerable<Comment>>))]
        public IEnumerable<IComment> CommentPreview { get; set; } = new List<IComment>();

        /// <summary>
        ///     Number of downvotes for the item.
        /// </summary>
        public int? Downs { get; set; }

        /// <summary>
        ///     Upvotes minus downvotes.
        /// </summary>
        public int? Points { get; set; }

        /// <summary>
        ///     Imgur popularity score.
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        ///     Topic of the gallery item.
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        ///     Topic ID of the gallery item.
        /// </summary>
        [JsonProperty("topic_id")]
        public int? TopicId { get; set; }

        /// <summary>
        ///     Upvotes for the item.
        /// </summary>
        public int? Ups { get; set; }
    }
}