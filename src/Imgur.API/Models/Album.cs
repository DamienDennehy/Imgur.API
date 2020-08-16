using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Imgur.API.Models
{
    /// <summary>
    /// An Album.
    /// </summary>
    public class Album : IAlbum
    {
        /// <summary>
        /// The ID for the Album.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The title of the Album.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the Album.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Utc timestamp of when the image was uploaded.
        /// </summary>
        public long DateTime { get; set; }

        /// <summary>
        /// The ID of the album cover image.
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// The height, in pixels, of the album cover image.
        /// </summary>
        [JsonPropertyName("cover_height")]
        public int? CoverHeight { get; set; }

        /// <summary>
        /// The width, in pixels, of the album cover image.
        /// </summary>
        [JsonPropertyName("cover_width")]
        public int? CoverWidth { get; set; }

        /// <summary>
        /// The account username or null if it's anonymous.
        /// </summary>
        [JsonPropertyName("account_url")]
        public string AccountUrl { get; set; }

        /// <summary>
        /// The account ID or null if it's anonymous.
        /// </summary>
        [JsonPropertyName("account_id")]
        public int? AccountId { get; set; }

        /// <summary>
        /// The privacy level of the album, you can only view public if not logged in as album owner.
        /// </summary>
        public string Privacy { get; set; }

        /// <summary>
        /// The view layout of the album.
        /// </summary>
        public string Layout { get; set; }

        /// <summary>
        /// The number of album views.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// The URL link to the album.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Indicates if the current user favorited the image. Defaults to false if not signed in.
        /// </summary>
        public bool? Favorite { get; set; }

        /// <summary>
        /// Indicates if the image has been marked as nsfw or not. Defaults to null if information is not available.
        /// </summary>
        public bool? Nsfw { get; set; }

        /// <summary>
        /// If the image has been categorized then this will contain the section the image belongs in. (funny, cats,
        /// adviceanimals, wtf, etc)
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// The total number of images in the album.
        /// </summary>
        [JsonPropertyName("images_count")]
        public int ImagesCount { get; set; }

        /// <summary>
        /// True if the image has been submitted to the gallery, false if otherwise.
        /// </summary>
        [JsonPropertyName("in_gallery")]
        public bool InGallery { get; set; }

        /// <summary>
        /// True if the image is an ad, false if otherwise.
        /// </summary>
        [JsonPropertyName("is_ad")]
        public bool IsAd { get; set; }

        /// <summary>
        /// OPTIONAL, the deletehash, if you're logged in as the album owner.
        /// </summary>
        public string DeleteHash { get; set; }

        /// <summary>
        /// A list of all the images in the album (only available when requesting the direct album).
        /// </summary>
        public IEnumerable<Image> Images { get; set; }
    }
}
