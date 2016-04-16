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
        public virtual int Followers { get; set; }

        /// <summary>
        ///     OPTIONAL, boolean representing whether or not the user is following the tag in their custom gallery.
        /// </summary>
        public virtual bool Following { get; set; }

        /// <summary>
        ///     A list of all the gallery items in the custom gallery
        /// </summary>
        [JsonConverter(typeof(TypeConverter<IEnumerable<GalleryItem>>))]
        public virtual IEnumerable<IGalleryItem> Items { get; set; }

        /// <summary>
        ///     Name of the tag.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Total number of gallery items for the tag.
        /// </summary>
        [JsonProperty("total_items")]
        public virtual int TotalItems { get; set; }
    }
}