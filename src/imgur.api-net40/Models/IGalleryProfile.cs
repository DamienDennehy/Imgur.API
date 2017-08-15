using System.Collections.Generic;

namespace Imgur.API.Models
{
    /// <summary>
    ///     The totals for a users gallery information.
    /// </summary>
    public interface IGalleryProfile
    {
        /// <summary>
        ///     Total number of comments the user has made in the gallery.
        /// </summary>
        int TotalGalleryComments { get; set; }

        /// <summary>
        ///     Total number of items favorited by the user in the gallery.
        /// </summary>
        int TotalGalleryFavorites { get; set; }

        /// <summary>
        ///     Total number of images submitted by the user.
        /// </summary>
        int TotalGallerySubmissions { get; set; }

        /// <summary>
        ///     A list of trophies that the user has.
        /// </summary>
        IEnumerable<ITrophy> Trophies { get; set; }
    }
}