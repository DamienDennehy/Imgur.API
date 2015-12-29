using Imgur.API.Enums;

namespace Imgur.API.Models
{
    /// <summary>
    ///     The data model formatted for gallery albums.
    /// </summary>
    public interface IGalleryAlbum : IGalleryItem, IAlbum
    {
        /// <summary>
        ///     The current user's vote on the item. null if not signed in or if the user hasn't voted on it.
        /// </summary>
        VoteOption? Vote { get; set; }
    }
}