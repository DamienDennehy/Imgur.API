using System.Collections.Generic;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     The totals for a users gallery information.
    /// </summary>
    public class GalleryProfile : IGalleryProfile
    {
        /// <summary>
        ///     Total number of comments the user has made in the gallery.
        /// </summary>
        [JsonProperty("total_gallery_comments")]
        public virtual int TotalGalleryComments { get; set; }

        /// <summary>
        ///     Total number of items favorited by the user in the gallery.
        /// </summary>
        [JsonProperty("total_gallery_favorites")]
        public virtual int TotalGalleryFavorites { get; set; }

        /// <summary>
        ///     Total number of images submitted by the user.
        /// </summary>
        [JsonProperty("total_gallery_submissions")]
        public virtual int TotalGallerySubmissions { get; set; }

        /// <summary>
        ///     A list of trophies that the user has.
        /// </summary>
        [JsonConverter(typeof(TypeConverter<IEnumerable<Trophy>>))]
        public virtual IEnumerable<ITrophy> Trophies { get; set; } = new List<ITrophy>();
    }
}