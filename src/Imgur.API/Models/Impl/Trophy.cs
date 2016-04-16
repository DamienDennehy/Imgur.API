using System;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     An earned Trophy.
    /// </summary>
    public class Trophy : ITrophy
    {
        /// <summary>
        ///     The ID of the image or the ID of the comment where the trophy was earned.
        /// </summary>
        public virtual string Data { get; set; }

        /// <summary>
        ///     A link to where the trophy was earned.
        /// </summary>
        [JsonProperty("data_link")]
        public virtual string DataLink { get; set; }

        /// <summary>
        ///     Utc timestamp of when the trophy was earned, converted from epoch time.
        /// </summary>
        [JsonConverter(typeof(EpochTimeConverter))]
        public virtual DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     A description of the trophy and how it was earned.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        ///     The ID of the trophy, this is unique to each trophy.
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        ///     URL of the image representing the trophy.
        /// </summary>
        public virtual string Image { get; set; }

        /// <summary>
        ///     The name of the trophy.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Can be thought of as the ID of a trophy type.
        /// </summary>
        [JsonProperty("name_clean")]
        public virtual string NameClean { get; set; }
    }
}