using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Custom Gallery related actions.
    /// </summary>
    public interface ICustomGalleryEndpoint : IEndpoint
    {
        /// <summary>
        ///     Add tags to a user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tags">The tags that should be added.</param>
        /// <returns></returns>
        Task<bool> AddCustomGalleryTagsAsync(IEnumerable<string> tags);

        /// <summary>
        ///     Add tags to filter out.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tag">The tag that should be filtered out.</param>
        /// <returns></returns>
        Task<bool> AddFilteredOutGalleryTagAsync(string tag);

        /// <summary>
        ///     View images for current user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <returns></returns>
        Task<ICustomGallery> GetCustomGalleryAsync(CustomGallerySortOrder? sort = CustomGallerySortOrder.Viral,
            TimeWindow? window = TimeWindow.Week, int? page = null);

        /// <summary>
        ///     View a single item in a user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        Task<IGalleryItem> GetCustomGalleryItemAsync(string galleryItemId);

        /// <summary>
        ///     Retrieve user's filtered out gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <returns></returns>
        Task<ICustomGallery> GetFilteredOutGalleryAsync(CustomGallerySortOrder? sort = CustomGallerySortOrder.Viral,
            TimeWindow? window = TimeWindow.Week, int? page = null);

        /// <summary>
        ///     Remove tags from a custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tags">The tags that should be removed.</param>
        /// <returns></returns>
        Task<bool> RemoveCustomGalleryTagsAsync(IEnumerable<string> tags);

        /// <summary>
        ///     Remove a filtered out tag.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tag">The tag that should be removed.</param>
        /// <returns></returns>
        Task<bool> RemoveFilteredOutGalleryTagAsync(string tag);
    }
}