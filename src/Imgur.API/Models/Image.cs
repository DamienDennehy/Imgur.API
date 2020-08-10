using System.Text.Json.Serialization;

namespace Imgur.API.Models
{
    /// <summary>
    /// An image.
    /// </summary>
    public class Image : IImage
    {
        /// <summary>
        /// The ID for the image.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The title of the image.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the image.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Utc timestamp of when the image was uploaded.
        /// </summary>
        public long DateTime { get; set; }

        /// <summary>
        /// Image MIME type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Is the image animated.
        /// </summary>
        public bool Animated { get; set; }

        /// <summary>
        /// The width of the image in pixels.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the image in pixels.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The size of the image in bytes.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The number of image views.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// Bandwidth consumed by the image in bytes.
        /// </summary>
        public long Bandwidth { get; set; }

        /// <summary>
        /// The current user's vote on the album. null if not signed in, if the user hasn't voted on it, or if not submitted to
        /// the gallery.
        /// </summary>
        public string Vote { get; set; }

        /// <summary>
        /// Indicates if the current user favorited the image. Defaults to false if not signed in.
        /// </summary>
        public bool Favorite { get; set; }

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
        /// The account url.
        /// </summary>
        [JsonPropertyName("account_url")]
        public string AccountUrl { get; set; }

        /// <summary>
        /// The account id.
        /// </summary>
        [JsonPropertyName("account_id")]
        public int? AccountId { get; set; }

        /// <summary>
        /// True if the image is an ad, false if otherwise.
        /// </summary>
        [JsonPropertyName("is_ad")]
        public bool IsAd { get; set; }

        /// <summary>
        /// True if the image is in most viral, false if otherwise.
        /// </summary>
        [JsonPropertyName("in_most_viral")]
        public bool InMostViral { get; set; }

        /// <summary>
        /// List of tags.
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// The Ad Type.
        /// </summary>
        [JsonPropertyName("ad_type")]
        public int AdType { get; set; }

        /// <summary>
        /// The ad url.
        /// </summary>
        [JsonPropertyName("ad_url")]
        public string AdUrl { get; set; }

        /// <summary>
        /// True if the image has been submitted to the gallery, false if otherwise.
        /// </summary>
        [JsonPropertyName("in_gallery")]
        public bool InGallery { get; set; }

        /// <summary>
        /// OPTIONAL, the deletehash, if you're logged in as the image owner.
        /// </summary>
        public string DeleteHash { get; set; }

        /// <summary>
        /// OPTIONAL, the original filename, if you're logged in as the image owner.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The direct link to the the image. (Note: if fetching an animated GIF that was over 20MB in original size, a .gif
        /// thumbnail will be returned)
        /// </summary>
        public string Link { get; set; }
    }
}
