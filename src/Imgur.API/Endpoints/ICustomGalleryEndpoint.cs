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
        /// <returns></returns>
        Task<bool> AddCustomGalleryTagsAsync(IEnumerable<string> tags);

        /// <summary>
        ///     Add tags to filter out.
        ///     OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<bool> AddFilteredOutGalleryTagAsync(string tag);

        /// <summary>
        ///     View images for current user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<ICustomGallery> GetCustomGalleryAsync(CustomGallerySortOrder sort = CustomGallerySortOrder.Viral,
            Window window = Window.Week, int? page = null);

        /// <summary>
        ///     View a single item in a user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<IGalleryItem> GetCustomGalleryItemAsync(string id);

        /// <summary>
        ///     Retrieve user's filtered out gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<ICustomGallery> GetFilteredOutGalleryAsync(CustomGallerySortOrder sort = CustomGallerySortOrder.Viral,
            Window window = Window.Week, int? page = null);

        /// <summary>
        ///     Remove tags from a custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveCustomGalleryTagsAsync(IEnumerable<string> tags);

        /// <summary>
        ///     Remove a filtered out tag.
        ///     OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveFilteredOutGalleryTagAsync(string tag);
    }
}