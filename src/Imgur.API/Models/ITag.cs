namespace Imgur.API.Models
{
    /// <summary>
    ///     The base model for a tag.
    /// </summary>
    public interface ITag
    {
        /// <summary>
        ///     Number of followers for the tag.
        /// </summary>
        int Followers { get; set; }

        /// <summary>
        ///     OPTIONAL, boolean representing whether or not the user is following the tag in their custom gallery.
        /// </summary>
        bool Following { get; set; }

        /// <summary>
        ///     An array of all the gallery items in the custom gallery
        /// </summary>
        IGalleryItem[] Items { get; set; }

        /// <summary>
        ///     Name of the tag.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Total number of gallery items for the tag.
        /// </summary>
        int TotalItems { get; set; }
    }
}