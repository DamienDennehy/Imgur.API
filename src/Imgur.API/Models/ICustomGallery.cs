namespace Imgur.API.Models
{
    /// <summary>
    ///     Represents a user's custom or filtered gallery.
    /// </summary>
    public interface ICustomGallery : IDataModel
    {
        /// <summary>
        ///     Username of the account that created the custom gallery.
        /// </summary>
        string AccountUrl { get; set; }

        /// <summary>
        ///     The total number of gallery items in the custom gallery.
        /// </summary>
        int ItemCount { get; set; }

        /// <summary>
        ///     An array of all the gallery items in the custom gallery
        /// </summary>
        IGalleryItem[] Items { get; set; }

        /// <summary>
        ///     The URL link to the custom gallery
        /// </summary>
        string Link { get; set; }

        /// <summary>
        ///     An array of all the tag names in the custom gallery.
        /// </summary>
        string[] Tags { get; set; }
    }
}