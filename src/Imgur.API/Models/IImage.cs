namespace Imgur.API.Models
{
    /// <summary>
    /// An image.
    /// </summary>
    public interface IImage : IDataModel
    {
        /// <summary>
        /// The ID for the image.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The title of the image.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Description of the image.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Utc timestamp of when the image was uploaded.
        /// </summary>
        long DateTime { get; }

        /// <summary>
        /// Image MIME type.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Is the image animated.
        /// </summary>
        bool Animated { get; }

        /// <summary>
        /// The width of the image in pixels.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// The height of the image in pixels.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// The size of the image in bytes.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// The number of image views.
        /// </summary>
        int Views { get; }

        /// <summary>
        /// Bandwidth consumed by the image in bytes.
        /// </summary>
        long Bandwidth { get; }

        /// <summary>
        /// The current user's vote on the album. null if not signed in, if the user hasn't voted on it, or if not submitted to
        /// the gallery.
        /// </summary>
        string Vote { get; }

        /// <summary>
        /// Indicates if the current user favorited the image. Defaults to false if not signed in.
        /// </summary>
        bool Favorite { get; }

        /// <summary>
        /// Indicates if the image has been marked as nsfw or not. Defaults to null if information is not available.
        /// </summary>
        bool Nsfw { get; }

        /// <summary>
        /// If the image has been categorized then this will contain the section the image belongs in. (funny, cats,
        /// adviceanimals, wtf, etc)
        /// </summary>
        string Section { get; }

        /// <summary>
        /// The account url.
        /// </summary>
        string AccountUrl { get; }

        /// <summary>
        /// The account id.
        /// </summary>
        string AccountId { get; }

        /// <summary>
        /// True if the image is an ad, false if otherwise.
        /// </summary>
        bool IsAd { get; }

        /// <summary>
        /// True if the image is in most viral, false if otherwise.
        /// </summary>
        bool InMostViral { get; }

        /// <summary>
        /// List of tags.
        /// </summary>
        string[] Tags { get; }

        /// <summary>
        /// The Ad Type.
        /// </summary>
        int AdType { get; }

        /// <summary>
        /// The ad url.
        /// </summary>
        string AdUrl { get; }

        /// <summary>
        /// True if the image has been submitted to the gallery, false if otherwise.
        /// </summary>
        bool InGallery { get; }

        /// <summary>
        /// OPTIONAL, the deletehash, if you're logged in as the image owner.
        /// </summary>
        string DeleteHash { get; }

        /// <summary>
        /// OPTIONAL, the original filename, if you're logged in as the image owner.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The direct link to the the image. (Note: if fetching an animated GIF that was over 20MB in original size, a .gif
        /// thumbnail will be returned)
        /// </summary>
        string Link { get; }
    }
}
