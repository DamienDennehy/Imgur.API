using System.Collections.Generic;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A tag.
    /// </summary>
    public class Tag : ITag
    {
        /// <summary>
        ///     Number of followers for the tag.
        /// </summary>
        public int Followers { get; set; }

        /// <summary>
        ///     OPTIONAL, boolean representing whether or not the user is following the tag in their custom gallery.
        /// </summary>
        public bool Following { get; set; }

        /// <summary>
        ///     A list of all the gallery items in the custom gallery
        /// </summary>
        [JsonConverter(typeof (TypeConverter<IEnumerable<GalleryItem>>))]
        public IEnumerable<IGalleryItem> Items { get; set; }

        /// <summary>
        ///     Name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Total number of gallery items for the tag.
        /// </summary>
        [JsonProperty("total_items")]
        public int TotalItems { get; set; }
    }
}