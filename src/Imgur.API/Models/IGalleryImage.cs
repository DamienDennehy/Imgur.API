namespace Imgur.API.Models
{
    /// <summary>
    ///     The data model formatted for gallery images.
    /// </summary>
    public interface IGalleryImage : IGalleryItem, IImage
    {
        /// <summary>
        ///     The account ID for the uploader, or null.
        /// </summary>
        int? AccountId { get; set; }

        /// <summary>
        ///     The username of the account that uploaded it, or null.
        /// </summary>
        string AccountUrl { get; set; }
    }
}