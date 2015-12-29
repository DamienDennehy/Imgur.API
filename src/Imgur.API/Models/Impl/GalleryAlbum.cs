using System;
using System.Collections.Generic;
using Imgur.API.Enums;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     The data model formatted for gallery albums.
    /// </summary>
    public class GalleryAlbum : GalleryItem, IGalleryAlbum
    {
        /// <summary>
        ///     The account ID of the account that uploaded it, or null.
        /// </summary>
        [JsonProperty("account_id")]
        public int? AccountId { get; set; }

        /// <summary>
        ///     The account username or null if it's anonymous.
        /// </summary>
        [JsonProperty("account_url")]
        public string AccountUrl { get; set; }

        /// <summary>
        ///     The ID of the album cover image.
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        ///     The height, in pixels, of the album cover image.
        /// </summary>
        [JsonProperty("cover_height")]
        public int? CoverHeight { get; set; }

        /// <summary>
        ///     The width, in pixels, of the album cover image.
        /// </summary>
        [JsonProperty("cover_width")]
        public int? CoverWidth { get; set; }

        /// <summary>
        ///     Utc timestamp of when the album was inserted into the gallery, converted from epoch time.
        /// </summary>
        [JsonConverter(typeof (EpochTimeConverter))]
        public DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     OPTIONAL, the deletehash, if you're logged in as the album owner.
        /// </summary>
        public string DeleteHash { get; set; }

        /// <summary>
        ///     The description of the album in the gallery.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Indicates if the current user favorited the album. Defaults to false if not signed in.
        /// </summary>
        public bool? Favorite { get; set; }

        /// <summary>
        ///     The ID for the image.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     A list of all the images in the album (only available when requesting the direct album).
        /// </summary>
        [JsonConverter(typeof (TypeConverter<IEnumerable<Image>>))]
        public IEnumerable<IImage> Images { get; set; } = new List<IImage>();

        /// <summary>
        ///     The total number of images in the album.
        /// </summary>
        [JsonProperty("images_count")]
        public int ImagesCount { get; set; }

        /// <summary>
        ///     The view layout of the album.
        /// </summary>
        public AlbumLayout? Layout { get; set; }

        /// <summary>
        ///     The URL link to the album.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        ///     Indicates if the image has been marked as nsfw or not. Defaults to null if information is not available.
        /// </summary>
        public bool? Nsfw { get; set; }

        /// <summary>
        ///     Order number of the album on the user's album page (defaults to 0 if their albums haven't been reordered).
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        ///     The privacy level of the album, you can only view public if not logged in as album owner.
        /// </summary>
        public AlbumPrivacy? Privacy { get; set; }

        /// <summary>
        ///     If the image has been categorized then this will contain the section the image belongs in. (funny, cats,
        ///     adviceanimals, wtf, etc)
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        ///     The title of the album in the gallery.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     The number of album views.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        ///     The current user's vote on the album. null if not signed in or if the user hasn't voted on it.
        /// </summary>
        public VoteOption? Vote { get; set; }
    }
}