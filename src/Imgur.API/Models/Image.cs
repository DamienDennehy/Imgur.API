using System.Text.Json.Serialization;

namespace Imgur.API.Models
{
    /// <summary>
    /// An image.
    /// </summary>
    public class Image : IImage
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long DateTime { get; set; }

        public string Type { get; set; }

        public bool Animated { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Size { get; set; }

        public int Views { get; set; }

        public long Bandwidth { get; set; }

        public string Vote { get; set; }

        public bool Favorite { get; set; }

        public bool Nsfw { get; set; }

        public string Section { get; set; }

        [JsonPropertyName("account_url")]
        public string AccountUrl { get; set; }

        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("is_ad")]
        public bool IsAd { get; set; }

        [JsonPropertyName("in_most_viral")]
        public bool InMostViral { get; set; }

        public string[] Tags { get; set; }

        [JsonPropertyName("ad_type")]
        public int AdType { get; set; }

        [JsonPropertyName("ad_url")]
        public string AdUrl { get; set; }

        [JsonPropertyName("in_gallery")]
        public bool InGallery { get; set; }

        public string DeleteHash { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }
    }
}
