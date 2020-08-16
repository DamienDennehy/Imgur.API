using System;
using System.Collections.Generic;

namespace Imgur.API.Models
{
    /// <summary>
    /// An Album.
    /// </summary>
    public interface IAlbum : IDataModel
    {
        /// <summary>
        /// The ID for the Album.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The title of the Album.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Description of the Album.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Utc timestamp of when the image was uploaded.
        /// </summary>
        long DateTime { get; }

        /// <summary>
        /// The ID of the album cover image.
        /// </summary>
        string Cover { get; set; }

        /// <summary>
        /// The height, in pixels, of the album cover image.
        /// </summary>
        int? CoverHeight { get; set; }

        /// <summary>
        /// The width, in pixels, of the album cover image.
        /// </summary>
        int? CoverWidth { get; set; }

        /// <summary>
        /// The account username or null if it's anonymous.
        /// </summary>
        string AccountUrl { get; set; }

        /// <summary>
        /// The account ID or null if it's anonymous.
        /// </summary>
        int? AccountId { get; set; }

        /// <summary>
        /// The privacy level of the album, you can only view public if not logged in as album owner.
        /// </summary>
        string Privacy { get; set; }

        /// <summary>
        /// The view layout of the album.
        /// </summary>
        string Layout { get; set; }

        /// <summary>
        /// The number of album views.
        /// </summary>
        int Views { get; set; }

        /// <summary>
        /// The URL link to the album.
        /// </summary>
        string Link { get; set; }

        /// <summary>
        /// Indicates if the current user favorited the image. Defaults to false if not signed in.
        /// </summary>
        bool? Favorite { get; set; }

        /// <summary>
        /// Indicates if the image has been marked as nsfw or not. Defaults to null if information is not available.
        /// </summary>
        bool? Nsfw { get; set; }

        /// <summary>
        /// If the image has been categorized then this will contain the section the image belongs in. (funny, cats,
        /// adviceanimals, wtf, etc)
        /// </summary>
        string Section { get; set; }

        /// <summary>
        /// The total number of images in the album.
        /// </summary>
        int ImagesCount { get; set; }

        /// <summary>
        /// True if the album has been submitted to the gallery, false if otherwise.
        /// </summary>
        bool InGallery { get; set; }

        /// <summary>
        /// True if the image is an ad, false if otherwise.
        /// </summary>
        bool IsAd { get; }

        /// <summary>
        /// OPTIONAL, the deletehash, if you're logged in as the album owner.
        /// </summary>
        string DeleteHash { get; set; }

        /// <summary>
        /// A list of all the images in the album (only available when requesting the direct album).
        /// </summary>
        IEnumerable<Image> Images { get; set; }
    }
}
