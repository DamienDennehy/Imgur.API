using System.Collections.Generic;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A user's custom or filtered gallery.
    /// </summary>
    public class CustomGallery : ICustomGallery
    {
        /// <summary>
        ///     Username of the account that created the custom gallery.
        /// </summary>
        [JsonProperty("account_url")]
        public virtual string AccountUrl { get; set; }

        /// <summary>
        ///     The total number of gallery items in the custom gallery.
        /// </summary>
        [JsonProperty("item_count")]
        public virtual int ItemCount { get; set; }

        /// <summary>
        ///     A list of all the gallery items in the custom gallery.
        /// </summary>
        [JsonConverter(typeof(TypeConverter<IEnumerable<GalleryItem>>))]
        public virtual IEnumerable<IGalleryItem> Items { get; set; } = new List<IGalleryItem>();

        /// <summary>
        ///     The URL link to the custom gallery.
        /// </summary>
        public virtual string Link { get; set; }

        /// <summary>
        ///     An list of all the tag names in the custom gallery.
        /// </summary>
        public virtual IEnumerable<string> Tags { get; set; } = new List<string>();
    }
}